using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BookingService.Data;
using Project.BookingServiceCommands.Commands;
using Project.BookingServiceQueries.Queries;
using Project.Common.Constants;
using Project.Common.Paging;
using Project.Core.Authentication;

namespace Project.BookingService.Controllers;

[Route("api/v{version:apiVersion}/[controller]/[Action]")]
[ApiController]
[ApiVersion("1")]
public class BookingPackageController : ControllerBase
{
    private readonly IMediator mediator;

    public BookingPackageController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetBookingPackageByID(Guid BookingPackageID)
    {
        return await mediator.Send(new GetBookingPackageByIDQuery(BookingPackageID));
    }
    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Supporter, RoleConstants.Admin })]
    public async Task<IActionResult> GetAllBookingPackageForAD([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] BookingStatus BookingStatus)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new GetAllBookingPackageForAdQuery(paginationRequestHeader, Response, BookingStatus));
    }
    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.User })]
    public async Task<IActionResult> GetAllBookingPackageForUser([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] BookingStatus BookingStatus)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
        return await mediator.Send(new GetAllBookingPackageForUserQuery(paginationRequestHeader, Response, userId, BookingStatus));
    }
    [HttpPut]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Supporter, RoleConstants.Admin })]
    public async Task<IActionResult> UpdateBookingPackageStatusDone(Guid BookingPackageID)
    {
        return await mediator.Send(new UpdateBookingStatusForBookingPackageCommand(BookingPackageID, BookingStatus.Done));
    }
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateBookingPackageStatusCancel(Guid BookingPackageID)
    {
        return await mediator.Send(new UpdateBookingStatusForBookingPackageCommand(BookingPackageID, BookingStatus.Cancel));
    }
}
