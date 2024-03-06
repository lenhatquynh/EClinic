using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Paging;
using Project.Core.Authentication;
using Project.ForumService.Commands;
using Project.ForumService.Dtos.CommentsDtos;
using Project.ForumService.Queries;

namespace Project.ForumService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [ApiVersion("1")]
    public class CommentController : ControllerBase
    {
        private readonly IMediator mediator;

        public CommentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComment([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] Guid PostID)
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID")?.Value;
            return await mediator.Send(new GetAllCommentQuery(paginationRequestHeader, Response, PostID, userId));
        }
        [HttpPost]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Doctor, RoleConstants.User, RoleConstants.Supporter })]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDtos createCommentDtos)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            return await mediator.Send(new CreateCommentCommands(createCommentDtos, userId));
        }
        [HttpPost]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Doctor, RoleConstants.User, RoleConstants.Supporter })]
        public async Task<IActionResult> CreateReplyComment([FromBody] CreateReplyCommentDtos createReplyCommentDtos)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            return await mediator.Send(new CreateReplyCommentCommands(createReplyCommentDtos, userId));
        }
        [HttpPut]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Doctor, RoleConstants.User, RoleConstants.Supporter })]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentDtos updateCommentDtos)
        {
            return await mediator.Send(new UpdateCommentCommands(updateCommentDtos));
        }
        [HttpPut]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Doctor, RoleConstants.User, RoleConstants.Supporter })]
        public async Task<IActionResult> UpdateReplyComment([FromBody] UpdateReplyCommentDtos updateReplyCommentDtos)
        {
            return await mediator.Send(new UpdateReplyCommentCommands(updateReplyCommentDtos));
        }
        [HttpDelete]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Doctor, RoleConstants.User, RoleConstants.Supporter })]
        public async Task<IActionResult> DeleteCommentByID(Guid CommentID)
        {
            return await mediator.Send(new DeleteCommentCommands(CommentID));
        }
        [HttpDelete]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Doctor, RoleConstants.User, RoleConstants.Supporter })]
        public async Task<IActionResult> DeleteReplyCommentByID(Guid ParentCommentID, Guid CommentID)
        {
            return await mediator.Send(new DeleteReplyCommentCommands(ParentCommentID, CommentID));
        }
        [HttpPut]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Doctor, RoleConstants.User })]
        public async Task<IActionResult> LikeComment(Guid CommentID)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            return await mediator.Send(new LikeCommentCommands(CommentID, userId));
        }
        [HttpPut]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Doctor, RoleConstants.User })]
        public async Task<IActionResult> LikeReplyComment(Guid ParentCommentID, Guid CommentID)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            return await mediator.Send(new LikeReplyCommentCommands(ParentCommentID, CommentID, userId));
        }
    }
}
