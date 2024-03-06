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
public class BookingDoctorController : ControllerBase
{
    private readonly IMediator mediator;

    public BookingDoctorController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Doctor })]
    public async Task<IActionResult> GetBookingDoctorByID(Guid BookingDoctorID)
    {
        return await mediator.Send(new GetBookingDoctorByIDQuery(BookingDoctorID));
    }
    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Doctor})]
    public async Task<IActionResult> GetAllBookingDoctorForDoctor([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] BookingStatus BookingStatus)
    {
        string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new GetAllBookingDoctorForDoctorQuery(userId,paginationRequestHeader, Response, BookingStatus));
    }
    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> GetAllBookingDoctorForAD([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] BookingStatus BookingStatus)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new GetAllBookingDoctorForAdQuery(paginationRequestHeader, Response, BookingStatus));
    }
    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.User })]
    public async Task<IActionResult> GetAllBookingDoctorForUser([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] BookingStatus BookingStatus)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
        return await mediator.Send(new GetAllBookingDoctorForUserQuery(paginationRequestHeader, Response, userId, BookingStatus));
    }
    [HttpPut]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Doctor, RoleConstants.Supporter, RoleConstants.Admin })]
    public async Task<IActionResult> UpdateBookingDoctorStatusDone(Guid BookingID)
    {
        return await mediator.Send(new UpdateBookingStatusForBookingDoctorCommand(BookingID, BookingStatus.Done));
    }
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateBookingDoctorStatusCancel(Guid BookingID)
    {
        return await mediator.Send(new UpdateBookingStatusForBookingDoctorCommand(BookingID, BookingStatus.Cancel));
    }
}
