using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BlogService.Commands;
using Project.BlogService.Dtos.BlogDtos;
using Project.BlogService.Dtos.BlogsDtos;
using Project.BlogService.Queries;
using Project.Common.Constants;
using Project.Common.Paging;
using Project.Core.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Project.BlogService.Controllers;

[Route("api/v{version:apiVersion}/[controller]/[Action]")]
[ApiController]
[ApiVersion("1")]
public class BlogController : ControllerBase
{
    private readonly IMediator mediator;

    public BlogController(IMediator mediator)
    {
        this.mediator = mediator;
    }
   
    [HttpPost]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> CreateBlog([FromForm] CreateBlogDtos createBlogDtos)
    {
        string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
        return await mediator.Send(new CreateBlogCommands(createBlogDtos, userId));
    }

    [HttpPost]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> UploadImage([Required] IFormFile image)
    {
        return await mediator.Send(new UploadImageCommands(image));
    }

    [HttpPut]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> UpdateBlog([FromForm] UpdateBlogDtos updateBlogDtos)
    {
        return await mediator.Send(new UpdateBlogCommands(updateBlogDtos));
    }

    [HttpDelete]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> DeleteBlogByID(Guid BlogID)
    {
        return await mediator.Send(new DeleteBlogCommands(BlogID));
    }

    [HttpGet]
    public async Task<IActionResult> GetBlogByID(Guid BlogID)
    {
        return await mediator.Send(new GetBlogQuery(BlogID));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBlog([FromHeader] int PageNumber, [FromHeader] int PageSize)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new GetAllBlogQuery(paginationRequestHeader, Response));
    }

    [HttpGet]
    public async Task<IActionResult> SearchBlog([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] SearchBlogDtos SearchBlogDtos)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new GetBlogsQuery(paginationRequestHeader, SearchBlogDtos, Response));
    }

    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> GetBlogByIDForAd(Guid BlogID)
    {
        return await mediator.Send(new GetBlogForAdQuery(BlogID));
    }

    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> GetAllBlogForAd([FromHeader] int PageNumber, [FromHeader] int PageSize)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new GetAllBlogForAdQuery(paginationRequestHeader, Response));
    }

    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> SearchBlogForAd([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] SearchBlogDtos SearchBlogDtos)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new GetBlogsForAdQuery(paginationRequestHeader, SearchBlogDtos, Response));
    }

}
