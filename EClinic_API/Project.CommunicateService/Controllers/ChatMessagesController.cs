using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Paging;
using Project.CommunicateService.Commands;
using Project.CommunicateService.Dtos.ChatMessageDtos;
using Project.CommunicateService.Queries;
using Project.Core.Authentication;

namespace Project.CommunicateService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [ApiVersion("1")]
    public class ChatMessagesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ChatMessagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Supporter, RoleConstants.Doctor, RoleConstants.User })]
        public async Task<IActionResult> GetALlMessageOfRoom([FromHeader] int PageNumber, [FromHeader] int PageSize, Guid RoomID)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetAllMessageOfRoomQuery(paginationRequestHeader, Response, RoomID, userId));
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Supporter, RoleConstants.Doctor, RoleConstants.User })]
        public async Task<IActionResult> GetAllImageOfRoom([FromHeader] int PageNumber, [FromHeader] int PageSize, Guid RoomID)
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetAllImageOfRoomQuery(paginationRequestHeader, Response, RoomID));
        }
        [HttpPost]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Supporter, RoleConstants.Doctor, RoleConstants.User })]
        public async Task<IActionResult> CreateMessage(CreateMessageDtos createMessageDtos)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            return await mediator.Send(new CreateMessageCommand(userId, createMessageDtos));
        }
        [HttpPost]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Supporter, RoleConstants.Doctor, RoleConstants.User })]
        public async Task<IActionResult> CreateMessageFile([FromForm] CreateMessageFileDtos createMessageFileDtos)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            return await mediator.Send(new CreateMessageFileCommand(userId, createMessageFileDtos));
        }

    }
}
