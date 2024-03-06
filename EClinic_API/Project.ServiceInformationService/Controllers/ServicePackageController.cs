using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Paging;
using Project.Core.Authentication;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Dtos.ServicePackageDTOs;
using Project.ServiceInformationService.Queries;

namespace Project.ServiceInformationService.Controllers;

[Route("api/v{version:apiVersion}/[controller]/[Action]")]
[ApiController]
[ApiVersion("1")]
public class ServicePackageController : ControllerBase
{
    private readonly IMediator mediator;

    public ServicePackageController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> CreateServicePackage([FromForm] CreateServicePackageDTO createServicePackageDTO)
    {
        return await mediator.Send(new CreateServicePackageCommand(createServicePackageDTO));
    }

    [HttpPut]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> UpdateServicePackage([FromForm] UpdateServicePackageDTO updateServicePackageDTO)
    {
        return await mediator.Send(new UpdateServicePackageCommand(updateServicePackageDTO));
    }

    [HttpPut]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> ToggleActiveServicePackage(Guid servicePackageID, bool flag)
    {
        return await mediator.Send(new ToggleActiveServicePackageCommand(servicePackageID, flag));
    }


    [HttpDelete]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> DeleteServicePackage(Guid deleteServicePackageID)
    {
        return await mediator.Send(new DeleteServicePackageCommand(deleteServicePackageID));
    }

    [HttpGet]
    public async Task<IActionResult> GetServicePackageByID(Guid servicePackageID)
    {
        return await mediator.Send(new GetServicePackageByIDQuery(servicePackageID));
    }

    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> GetServicePackageByIDForAd(Guid servicePackageID)
    {
        return await mediator.Send(new GetServicePackageByIDForAdQuery(servicePackageID));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllServicePackage([FromHeader] int PageNumber, [FromHeader] int PageSize)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new GetAllServicePackageQuery(paginationRequestHeader, Response));
    }

    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> GetAllServicePackageForAd([FromHeader] int PageNumber, [FromHeader] int PageSize)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new GetAllServicePackageForAdQuery(paginationRequestHeader, Response));
    }

    [HttpGet]
    public async Task<IActionResult> SearchServicePackage([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] SearchServicePackageDTO searchServicePackageDTO)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new SearchServicePackageQuery(paginationRequestHeader, searchServicePackageDTO, Response));
    }

    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> SearchServicePackageForAd([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] SearchServicePackageDTO searchServicePackageDTO)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new SearchServicePackageForAdQuery(paginationRequestHeader, searchServicePackageDTO, Response));
    }

    [HttpGet]
    public async Task<IActionResult> SearchServicePackageFiltered([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] SearchServicePackageFilteredDTO searchServicePackageFilteredDTO)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new SearchServicePackageFilteredQuery(paginationRequestHeader, searchServicePackageFilteredDTO, Response));
    }

}
