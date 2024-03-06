using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.PaymentService.Model;
using Project.PaymentService.Protos;
using Project.PaymentService.Queries;
using Project.PaymentService.Repository.PaymentRepositories;

namespace Project.RefundService.Handlers.RefundHandlers
{
    public class GetPaymentByIDHandler : IRequestHandler<GetPaymentByIDQuery, ObjectResult>
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly ILogger<GetPaymentByIDHandler> logger;
        private readonly IMapper mapper;
        private readonly ProfileService.ProfileServiceClient client;
        public GetPaymentByIDHandler(IConfiguration configuration, IPaymentRepository paymentRepository, ILogger<GetPaymentByIDHandler> logger, IMapper mapper)
        {
            this.paymentRepository = paymentRepository;
            this.logger = logger;
            this.mapper = mapper;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(GetPaymentByIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Payment = await paymentRepository.GetPayment(request.PaymentID);
                var paymentDtos = mapper.Map<PaymentDetailDtos>(Payment);
                var response = await client.GetProfileAsync(new GetProfileRequest { UserID = Payment.UserID.ToString() });
                if (response.FirstName =="")
                {
                    return ApiResponse.NotFound("Get Payemnt Error");
                }
                paymentDtos.Author = new Author();
                paymentDtos.Author.UserID = Guid.Parse(response.UserID);
                paymentDtos.Author.FirstName = response.FirstName;
                paymentDtos.Author.LastName = response.LastName;
                paymentDtos.Author.Avatar = response.Avatar;
                return ApiResponse.OK(paymentDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
