﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BlogService.Commands;
using Project.BlogService.Dtos.HashtagDtos;
using Project.BlogService.Queries;
using Project.Common.Constants;
using Project.Common.Paging;
using Project.Core.Authentication;

namespace Project.BlogService.Controllers;

[Route("api/v{version:apiVersion}/[controller]/[Action]")]
[ApiController]
[ApiVersion("1")]
public class HashtagController : ControllerBase
{
    private readonly IMediator mediator;

    public HashtagController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> GetTagSortByCount([FromHeader] int PageNumber, [FromHeader] int PageSize)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new GetTagSortByCountQuery(paginationRequestHeader, Response));
    }
    [HttpGet]
    public async Task<IActionResult> GetAllHashtag()
    {
        return await mediator.Send(new GetAllHashtagQuery());
    }
    [HttpPost]
    public async Task<IActionResult> CreateHashtag(string HashtagName)
    {
        return await mediator.Send(new CreateHashtagCommands(HashtagName));
    }
    [HttpPut]
    public async Task<IActionResult> UpdateHashtag(UpdateHashtagDtos updateHashtagDtos)
    {
        return await mediator.Send(new UpdateHashtagCommands(updateHashtagDtos));
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteHashtag(Guid HashtagID)
    {
        return await mediator.Send(new DeleteHashtagCommands(HashtagID));
    }
}
