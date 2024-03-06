using Grpc.Net.Client;
using MassTransit;
using MediatR;
using Project.Common.Constants;
using Project.Core.Logger;
using Project.Core.RabbitMQ;
using Project.NotificationService.Dtos;
using Project.PaymentService.Commands;
using Project.PaymentService.Data;
using Project.PaymentService.Model;
using Project.PaymentService.MomoPayment;
using Project.PaymentService.Protos;
using Project.PaymentService.Repository.PaymentRepositories;
using Project.PaymentService.VNPayPayment;

namespace Project.PaymentService.Handlers.PaymentHandlers
{
    public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, string>
    {
        private readonly ILogger<CreatePaymentHandler> logger;
        private readonly IMomoPayment momoPayment;
        private readonly IVNPayPayment vNPayPayment;
        private readonly IPaymentRepository paymentRepository;
        private readonly string clientAddress;
        private readonly BookingService.BookingServiceClient client;
        private readonly ProfileService.ProfileServiceClient profileclient;
        private readonly IBus bus;

        public CreatePaymentHandler(IBus bus, IConfiguration configuration, ILogger<CreatePaymentHandler> logger, IMomoPayment momoPayment, IVNPayPayment vNPayPayment, IPaymentRepository paymentRepository)
        {
            this.logger = logger;
            this.momoPayment = momoPayment;
            this.vNPayPayment = vNPayPayment;
            this.paymentRepository = paymentRepository;
            clientAddress = configuration.GetValue<string>("ClinentUrl");
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:BookingServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new BookingService.BookingServiceClient(channel);
            GrpcChannel channel2 = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            profileclient = new ProfileService.ProfileServiceClient(channel2);
            this.bus = bus;
        }

        public async Task<string> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string url = null;
                PaymentResult paymentResult = null;
                switch (request.PaymentService)
                {

                    case Data.PaymentService.Momo:
                        paymentResult = momoPayment.PaymentConfirm(request.QueryParameters);
                        break;

                    case Data.PaymentService.VNPay:
                        paymentResult = vNPayPayment.PaymentConfirm(request.QueryParameters);
                        break;
                    default:
                        paymentResult = null;
                        break;
                }
                if (paymentResult == null)
                {
                    return null;
                }
                var type = paymentResult.PaymentType;
                if (type == "BookingPackage" && paymentResult.IsSuccess == true)
                {
                    var updateResult = await client.UpdateBookingPackageAsync(new UpdateBookingPackageRequest { BookingPackageID = paymentResult.BookingID.ToString() });
                    url = $"{clientAddress}/services/{updateResult.UserID}?bookingId={paymentResult.BookingID}&status=success";
                }
                if (type == "BookingDoctor" && paymentResult.IsSuccess == true)
                {
                    var updateResult = await client.UpdateBookingDoctorAsync(new UpdateBookingDoctorRequest { BookingDoctorID = paymentResult.BookingID.ToString() });
                    url = $"{clientAddress}/doctors/{updateResult.UserID}?bookingId={paymentResult.BookingID}&status=success";
                }
                if (type == "BookingPackage" && paymentResult.IsSuccess == false)
                {
                    var updateResult = await client.GetBookingPackageAsync(new GetBookingPackageRequest { BookingPackageID = paymentResult.BookingID.ToString() });
                    url = $"{clientAddress}/services/{updateResult.UserID}?bookingId={paymentResult.BookingID}&status=fail";
                    return url;
                }
                if (type == "BookingDoctor" && paymentResult.IsSuccess == false)
                {
                    var updateResult = await client.GetBookingDoctorAsync(new GetBookingDoctorRequest { BookingDoctorID = paymentResult.BookingID.ToString() });
                    url = $"{clientAddress}/doctors/{updateResult.UserID}?bookingId={paymentResult.BookingID}&status=fail";
                    return url;
                }
                Payment payment = new Payment
                {
                    BookingID = paymentResult.BookingID,
                    PaymentAmount = paymentResult.Amount,
                    PaymentService = request.PaymentService,
                    PaymentTime = paymentResult.PaymentTime,
                    TransactionID = paymentResult.TransactionID,
                    UserID = paymentResult.UserID,
                    OrderID = paymentResult.OrderID
                };
                var result = await paymentRepository.CreateAsync(payment);
                if (!result)
                {
                    return clientAddress;
                }
                var res = await profileclient.GetProfileAsync(new GetProfileRequest { UserID = payment.UserID.ToString() });
                await bus.SendMessageWithExchangeName<PaymentModelData>(new PaymentModelData
                {
                    FullName = res.FirstName + " " + res.LastName,
                    BookingType = type,
                    PaymentAmount = paymentResult.Amount,
                    PaymentID = payment.PaymentID,
                    PaymentService = request.PaymentService == Data.PaymentService.Momo ? "Momo" : "VNPay",
                    PaymentTime = paymentResult.PaymentTime,
                    TransactionID = paymentResult.TransactionID,
                    Email = res.Email
                }, ExchangeConstants.NotificationService + "SendBill");
                return url;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }
    }
}
