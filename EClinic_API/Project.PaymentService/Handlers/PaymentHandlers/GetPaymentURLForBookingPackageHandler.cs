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
    public class GetPaymentURLForBookingPackageHandler : IRequestHandler<GetPaymentURLForBookingPackageQuery, ObjectResult>
    {
        private readonly ILogger<GetPaymentURLForBookingPackageHandler> logger;
        private readonly IMomoPayment momoPayment;
        private readonly IVNPayPayment vNPayPayment;
        private readonly BookingService.BookingServiceClient client;
        public GetPaymentURLForBookingPackageHandler(IConfiguration configuration, ILogger<GetPaymentURLForBookingPackageHandler> logger, IMomoPayment momoPayment, IVNPayPayment vNPayPayment)
        {
            this.logger = logger;
            this.momoPayment = momoPayment;
            this.vNPayPayment = vNPayPayment;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:BookingServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new BookingService.BookingServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(GetPaymentURLForBookingPackageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                CreateBookingPackageRequest packageRequest = new CreateBookingPackageRequest
                {
                    ProfileID = request.BookingPackageDtos.ProfileID.ToString(),
                    UserID = request.BookingPackageDtos.UserID.ToString(),
                    AppoinmentTime = request.BookingPackageDtos.AppoinmentTime.ToString(),
                    Price = request.BookingPackageDtos.Price,
                    ServicePackageID = request.BookingPackageDtos.ServicePackageID.ToString()
                };
                var createResult = await client.CreateBookingPackageAsync(packageRequest);
                if(createResult == null)
                {
                    return ApiResponse.InternalServerError();
                }
                PaymentModel paymentModel = new PaymentModel
                {
                    BookingID = Guid.Parse(createResult.BookingPackageID),
                    UserID = request.BookingPackageDtos.UserID,
                    Amount = request.BookingPackageDtos.Price,
                    Message = "BookingPackage"
                };
                string url = "";
                switch (request.PaymentService)
                {
                    case Data.PaymentService.Momo:
                        url = await momoPayment.PaymentRequest(paymentModel);
                        break;

                    case Data.PaymentService.VNPay:
                        url = vNPayPayment.PaymentRequest(paymentModel,request.IpAddress);
                        break;
                    default:
                        url = null;
                        break;
                }
                if(url == null)
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
