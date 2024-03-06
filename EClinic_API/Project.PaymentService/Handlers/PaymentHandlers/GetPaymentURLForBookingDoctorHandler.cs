using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.PaymentService.Model;
using Project.PaymentService.MomoPayment;
using Project.PaymentService.Protos;
using Project.PaymentService.Queries;
using Project.PaymentService.VNPayPayment;

namespace Project.PaymentService.Handlers.PaymentHandlers
{
    public class GetPaymentURLForBookingDoctorHandler : IRequestHandler<GetPaymentURLForBookingDoctorQuery, ObjectResult>
    {
        private readonly ILogger<GetPaymentURLForBookingDoctorHandler> logger;
        private readonly IMomoPayment momoPayment;
        private readonly IVNPayPayment vNPayPayment;
        private readonly BookingService.BookingServiceClient client;
        public GetPaymentURLForBookingDoctorHandler(IConfiguration configuration, ILogger<GetPaymentURLForBookingDoctorHandler> logger, IMomoPayment momoPayment, IVNPayPayment vNPayPayment)
        {
            this.logger = logger;
            this.momoPayment = momoPayment;
            this.vNPayPayment = vNPayPayment;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:BookingServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new BookingService.BookingServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(GetPaymentURLForBookingDoctorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                CreateBookingDoctorRequest doctorRequest = new CreateBookingDoctorRequest
                {
                    ProfileID = request.BookingDoctorDtos.ProfileID.ToString(),
                    UserID = request.BookingDoctorDtos.UserID.ToString(),
                    BookingType = (int)request.BookingDoctorDtos.BookingType,
                    Price = request.BookingDoctorDtos.Price,
                    DoctorID = request.BookingDoctorDtos.DoctorID.ToString(),
                    ScheduleID = request.BookingDoctorDtos.ScheduleID.ToString()
                };
                var createResult = await client.CreateBookingDoctorAsync(doctorRequest);
                if (createResult == null)
                {
                    return ApiResponse.InternalServerError();
                }
                PaymentModel paymentModel = new PaymentModel
                {
                    BookingID = Guid.Parse(createResult.BookingDoctorID),
                    UserID = request.BookingDoctorDtos.UserID,
                    Amount = request.BookingDoctorDtos.Price,
                    Message = "BookingDoctor"
                };
                string url = "";
                switch (request.PaymentService)
                {
                    case Data.PaymentService.Momo:
                        url = await momoPayment.PaymentRequest(paymentModel);
                        break;

                    case Data.PaymentService.VNPay:
                        url = vNPayPayment.PaymentRequest(paymentModel, request.IpAddress);
                        break;
                    default:
                        url = null;
                        break;
                }
                if (url == null)
                {
                    throw new Exception("Get payment url error");
                }
                return ApiResponse.OK(url);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
