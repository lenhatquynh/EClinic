using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Paging;
using Project.Core.Authentication;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Dtos.ServiceDTOs;
using Project.ServiceInformationService.Queries;

namespace Project.ServiceInformationService.Controllers;

[Route("api/v{version:apiVersion}/[controller]/[Action]")]
[ApiController]
[ApiVersion("1")]
public class ServiceController : ControllerBase
{
    private readonly IMediator mediator;

    public ServiceController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> CreateService([FromForm] CreateServiceDTO createServiceDTO)
    {
        return await mediator.Send(new CreateServiceCommand(createServiceDTO));
    }

    [HttpPut]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> UpdateService([FromForm] UpdateServiceDTO updateServiceDTO)
    {
        return await mediator.Send(new UpdateServiceCommand(updateServiceDTO));
    }

    [HttpPut]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> ToggleActiveService(Guid serviceID, bool flag)
    {
        return await mediator.Send(new ToggleActiveServiceCommand(serviceID, flag));
    }

    [HttpDelete]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> DeleteService(Guid deleteServiceID)
    {
        return await mediator.Send(new DeleteServiceCommand(deleteServiceID));
    }

    [HttpGet]
    public async Task<IActionResult> GetServiceByID(Guid serviceID)
    {
        return await mediator.Send(new GetServiceByIDQuery(serviceID));
    }

    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> GetServiceByIDForAd(Guid serviceID)
    {
        return await mediator.Send(new GetServiceByIDForAdQuery(serviceID));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllService([FromHeader] int PageNumber, [FromHeader] int PageSize)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new GetAllServiceQuery(paginationRequestHeader, Response));
    }

    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> GetAllServiceForAd([FromHeader] int PageNumber, [FromHeader] int PageSize)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new GetAllServiceForAdQuery(paginationRequestHeader, Response));
    }

    [HttpGet]
    public async Task<IActionResult> SearchService([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] SearchServiceDTO searchServiceDTO)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new SearchServiceQuery(paginationRequestHeader, searchServiceDTO, Response));
    }

    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> SearchServiceForAd([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] SearchServiceDTO searchServiceDTO)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new SearchServiceForAdQuery(paginationRequestHeader, searchServiceDTO, Response));
    }

}
