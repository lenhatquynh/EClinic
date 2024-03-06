using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Logger;
using Project.PaymentService.Model;
using Project.PaymentService.Queries;
using Project.PaymentService.Repository.RefundRepositories;

namespace Project.PaymentService.Handlers.RefundHandlers
{
    public class GetAllRefundHandler : IRequestHandler<GetAllRefundQuery, ObjectResult>
    {
        private readonly IRefundRepository refundRepository;
        private readonly ILogger<GetAllRefundHandler> logger;
        private readonly IMapper mapper;

        public GetAllRefundHandler(IRefundRepository refundRepository, ILogger<GetAllRefundHandler> logger, IMapper mapper)
        {
            this.refundRepository = refundRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<ObjectResult> Handle(GetAllRefundQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var refunds = await refundRepository.GetAllRefund(x => true);
                if (refunds == null)
                {
                    return ApiResponse.NotFound("Payment not found");
                }
                PaginationResponseHeader header = new PaginationResponseHeader();
                header.TotalCount = refunds.Count;
                refunds = refunds.OrderByDescending(x => x.RefundTime)
                    .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                    .Take(request.PaginationRequestHeader.PageSize).ToList();
                header.PageIndex = request.PaginationRequestHeader.PageNumber;
                header.PageSize = request.PaginationRequestHeader.PageSize;
                request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
                var refundDtos = mapper.Map<List<RefundDtos>>(refunds);
                return ApiResponse.OK<List<RefundDtos>>(refundDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
