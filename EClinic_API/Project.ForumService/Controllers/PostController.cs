using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Paging;
using Project.Core.Authentication;
using Project.ForumService.Commands;
using Project.ForumService.Dtos.PostsDtos;
using Project.ForumService.Queries;

namespace Project.ForumService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [ApiVersion("1")]
    public class PostController : ControllerBase
    {
        private readonly IMediator mediator;

        public PostController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetPostsSortByLike([FromHeader] int PageNumber, [FromHeader] int PageSize)
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetPostsSortByLikeQuery(paginationRequestHeader, Response));
        }
        [HttpGet]
        public async Task<IActionResult> SearchPostOfHashTag([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] string searchText)
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetPostsOfHashTagQuery(paginationRequestHeader, searchText, Response));
        }
        [HttpGet]
        public async Task<IActionResult> SearchPost([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] SearchPostDtos SearchPostDtos)
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetPostsQuery(paginationRequestHeader, SearchPostDtos, Response));
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> GetAllPost([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] string SearchText)
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetAllPostQuery(paginationRequestHeader, Response, SearchText));
        }
        [HttpGet]
        public async Task<IActionResult> GetPostNotActive([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] string SearchText)
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetPostNotActiveQuery(paginationRequestHeader, Response, SearchText));
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.User, RoleConstants.Admin, RoleConstants.Supporter })]
        public async Task<IActionResult> GetPostOfUser([FromHeader] int PageNumber, [FromHeader] int PageSize)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID")?.Value;
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetPostOfUserQuery(paginationRequestHeader, Response, userId));
        }
        [HttpGet]
        //[CustomAuthorize(Authorities = new[] { RoleConstants.Doctor, RoleConstants.Admin, RoleConstants.Supporter })]
        public async Task<IActionResult> GetPostNoAnswer([FromHeader] int PageNumber, [FromHeader] int PageSize)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID")?.Value;
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetPostNoAnswerQuery(paginationRequestHeader, Response));
        }
        [HttpGet]
        public async Task<IActionResult> GetPostByID(Guid PostID)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID")?.Value;
            return await mediator.Send(new GetPostQuery(PostID, userId));
        }
        [HttpPost]
        [CustomAuthorize(Authorities = new[] { RoleConstants.User, RoleConstants.Admin, RoleConstants.Supporter })]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDtos createPostDtos)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            return await mediator.Send(new CreatePostCommands(createPostDtos, userId));
        }
        [HttpPut]
        [CustomAuthorize(Authorities = new[] { RoleConstants.User, RoleConstants.Admin, RoleConstants.Supporter })]
        public async Task<IActionResult> UpdatePost([FromForm] UpdatePostDtos updatePostDtos)
        {
            return await mediator.Send(new UpdatePostCommands(updatePostDtos));
        }
        [HttpDelete]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.User })]
        public async Task<IActionResult> DeletePostByID(Guid PostID)
        {
            return await mediator.Send(new DeletePostCommands(PostID));
        }
        [HttpPut]
        [CustomAuthorize(Authorities = new[] { RoleConstants.User, RoleConstants.Admin, RoleConstants.Supporter, RoleConstants.Doctor })]
        public async Task<IActionResult> LikePost(Guid PostID)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            return await mediator.Send(new LikePostCommands(PostID, userId));
        }
        [HttpPut]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Supporter, RoleConstants.Admin })]
        public async Task<IActionResult> ChangeActivePost(Guid PostID)
        {
            return await mediator.Send(new AcceptPostCommands(PostID));
        }
    }
}
