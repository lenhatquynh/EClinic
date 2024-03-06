using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.PaymentService.Model;
using Project.PaymentService.Queries;
using Project.PaymentService.Repository.RefundRepositories;

namespace Project.RefundService.Handlers.RefundHandlers
{
    public class GetRefundByIDHandler : IRequestHandler<GetRefundByIDQuery, ObjectResult>
    {
        private readonly IRefundRepository refundRepository;
        private readonly ILogger<GetRefundByIDHandler> logger;
        private readonly IMapper mapper;

        public GetRefundByIDHandler(IRefundRepository refundRepository, ILogger<GetRefundByIDHandler> logger, IMapper mapper)
        {
            this.refundRepository = refundRepository;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<ObjectResult> Handle(GetRefundByIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Refunds = await refundRepository.GetRefund(request.RefundID);
                var RefundDtos = mapper.Map<RefundDetailDtos>(Refunds);
                return ApiResponse.OK(RefundDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
