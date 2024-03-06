using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BookingService.Dtos.DoctorScheduleDtos;
using Project.BookingServiceCommands.Commands;
using Project.BookingServiceQueries.Queries;
using Project.Common.Constants;
using Project.Core.Authentication;

namespace Project.BookingService.Controllers;

[Route("api/v{version:apiVersion}/[controller]/[Action]")]
[ApiController]
[ApiVersion("1")]
public class DoctorScheduleController : ControllerBase
{
    private readonly IMediator mediator;

    public DoctorScheduleController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Supporter, RoleConstants.Admin, RoleConstants.Doctor})]
    public async Task<IActionResult> GetDoctorScheduleForAD(DateTime Date, Guid DoctorID)
    {
        return await mediator.Send(new GetDoctorScheduleByDayForAdQuery(Date, DoctorID));
    }
    [HttpGet]
    public async Task<IActionResult> GetDoctorScheduleForUser(DateTime Date, Guid DoctorID)
    {
        return await mediator.Send(new GetDoctorScheduleByDayForUserQuery(Date, DoctorID));
    }
    [HttpPost]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Supporter, RoleConstants.Admin})]
    public async Task<IActionResult> CreateDoctorSchedule(CreateDoctorScheduleDtos CreateDoctorScheduleDtos)
    {
        return await mediator.Send(new CreateDoctorScheduleCommand(CreateDoctorScheduleDtos));
    }
    [HttpPut]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Supporter, RoleConstants.Admin})]
    public async Task<IActionResult> UpdateDoctorSchedule(UpdateDoctorScheduleDtos UpdateDoctorScheduleDtos)
    {
        return await mediator.Send(new UpdateDoctorScheduleCommand(UpdateDoctorScheduleDtos));
    }
}
