using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BookingServiceQueries.Queries;
using Project.Common.Constants;
using Project.Core.Authentication;

namespace Project.BookingService.Controllers;

[Route("api/v{version:apiVersion}/[controller]/[Action]")]
[ApiController]
[ApiVersion("1")]
public class BookingController : ControllerBase
{
    private readonly IMediator mediator;

    public BookingController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
    public async Task<IActionResult> GetStatisticsOverview()
    {
        return await mediator.Send(new GetStatisticsOverviewQuery());
    }

}
