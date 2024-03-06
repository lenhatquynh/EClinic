using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Logger;
using Project.PaymentService.Model;
using Project.PaymentService.Queries;
using Project.PaymentService.Repository.PaymentRepositories;

namespace Project.PaymentService.Handlers.PaymentHandlers
{
    public class GetAllPaymentHandler : IRequestHandler<GetAllPaymentQuery, ObjectResult>
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly ILogger<GetAllPaymentHandler> logger;
        private readonly IMapper mapper;

        public GetAllPaymentHandler(IPaymentRepository paymentRepository, ILogger<GetAllPaymentHandler> logger, IMapper mapper)
        {
            this.paymentRepository = paymentRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<ObjectResult> Handle(GetAllPaymentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var payments = await paymentRepository.GetAllPayment(x => true);
                if (payments == null)
                {
                    return ApiResponse.NotFound("Payment not found");
                }
                PaginationResponseHeader header = new PaginationResponseHeader();
                header.TotalCount = payments.Count;
                payments = payments.OrderByDescending(x => x.PaymentTime)
                    .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                    .Take(request.PaginationRequestHeader.PageSize).ToList();
                header.PageIndex = request.PaginationRequestHeader.PageNumber;
                header.PageSize = request.PaginationRequestHeader.PageSize;
                request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
                var paymentDtos = mapper.Map<List<PaymentDtos>>(payments);
                return ApiResponse.OK<List<PaymentDtos>>(paymentDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
