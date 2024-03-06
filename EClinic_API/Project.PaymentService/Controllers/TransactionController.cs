using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Paging;
using Project.Core.Authentication;
using Project.PaymentService.Commands;
using Project.PaymentService.Model;
using Project.PaymentService.Queries;

namespace Project.PaymentService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [ApiVersion("1")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator mediator;

        public TransactionController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> GetStatisticsOverview()
        {
            return await mediator.Send(new GetStatisticsOverviewQuery());
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> GetPaymentByID(Guid PaymentID)
        {
            return await mediator.Send(new GetPaymentByIDQuery(PaymentID));
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> GetRefundByID(Guid RefundID)
        {
            return await mediator.Send(new GetRefundByIDQuery(RefundID));
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> GetAllPaymentTransaction([FromHeader] int PageNumber, [FromHeader] int PageSize)
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetAllPaymentQuery(paginationRequestHeader, Response));
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> GetAllRefundTransaction([FromHeader] int PageNumber, [FromHeader] int PageSize)
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetAllRefundQuery(paginationRequestHeader, Response));
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> GetTransactionQuery(DateTime StartTime, DateTime EndTime, TimeType timeType)
        {
            TransactionQueryModel query = new TransactionQueryModel
            {
                StartTime = StartTime,
                TimeType = timeType,
                EndTime = EndTime
            };
            return await mediator.Send(new GetTransactionQuery(query));
        }
        [HttpPost]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> RefundTransaction(RefundModel RefundModel)
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            return await mediator.Send(new CreateRefundCommand(RefundModel, ipAddress));
        }
    }
}
