using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Paging;
using Project.CommunicateService.Commands;
using Project.CommunicateService.Queries;
using Project.Core.Authentication;

namespace Project.CommunicateService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [ApiVersion("1")]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator mediator;

        public RoomsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Supporter })]
        public async Task<IActionResult> GetAllNewRoom([FromHeader] int PageNumber, [FromHeader] int PageSize)
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetAllNewRoomQuery(paginationRequestHeader, Response));
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Doctor })]
        public async Task<IActionResult> GetAllRoomOfDoctor([FromHeader] int PageNumber, [FromHeader] int PageSize)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetAllRoomOfDoctorQuery(paginationRequestHeader, Response, userId));
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Supporter })]
        public async Task<IActionResult> GetAllRoomOfSupporter([FromHeader] int PageNumber, [FromHeader] int PageSize)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetAllRoomOfSupporterQuery(paginationRequestHeader, Response, userId));
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.User })]
        public async Task<IActionResult> GetAllRoomOfUser([FromHeader] int PageNumber, [FromHeader] int PageSize)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetAllRoomQuery(paginationRequestHeader, Response, userId));
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.User, RoleConstants.Doctor, RoleConstants.Supporter })]
        public async Task<IActionResult> SearchRoom([FromHeader] int PageNumber, [FromHeader] int PageSize, string SearchText)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new SearchRoomQuery(paginationRequestHeader, Response, userId, SearchText));
        }
        [HttpPost]
        [CustomAuthorize(Authorities = new[] { RoleConstants.User })]
        public async Task<IActionResult> CreateSupporterRoom(string Message)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            return await mediator.Send(new CreateSupporterRoomCommand(Message, userId));
        }
        //[HttpPost]
        //[CustomAuthorize(Authorities = new[] { RoleConstants.User, RoleConstants.Supporter, RoleConstants.Doctor })]
        //public async Task<IActionResult> CreateDoctorRoom()
        //{
        //    return await mediator.Send(new CreateDoctorRoomCommand());
        //}
        [HttpPut]
        [CustomAuthorize(Authorities = new[] { RoleConstants.User, RoleConstants.Supporter, RoleConstants.Doctor })]
        public async Task<IActionResult> CloseRoom(Guid RoomID)
        {
            return await mediator.Send(new CloseRoomCommand(RoomID));
        }
    }
}
