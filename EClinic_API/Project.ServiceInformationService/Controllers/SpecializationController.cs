using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Paging;
using Project.Core.Authentication;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Dtos.SpecializationDTOs;
using Project.ServiceInformationService.Queries;

namespace Project.ServiceInformationService.Controllers;

[Route("api/v{version:apiVersion}/[controller]/[Action]")]
[ApiController]
[ApiVersion("1")]
public class SpecializationController : ControllerBase
{
    private readonly IMediator mediator;

    public SpecializationController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> CreateSpecialization([FromForm] CreateSpecializationDTO createSpecializationDTO)
    {
        return await mediator.Send(new CreateSpecializationCommand(createSpecializationDTO));
    }

    [HttpPut]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> UpdateSpecialization([FromForm] UpdateSpecializationDTO updateSpecializationDTO)
    {
        return await mediator.Send(new UpdateSpecializationCommand(updateSpecializationDTO));
    }

    [HttpDelete]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> DeleteSpecialization([FromForm] DeleteSpecializationDTO deleteSpecializationDTO)
    {
        return await mediator.Send(new DeleteSpecializationCommand(deleteSpecializationDTO));
    }

    [HttpGet]
    [CustomAuthorize(Authorities = new[] { RoleConstants.Admin, RoleConstants.Supporter })]
    public async Task<IActionResult> GetSpecializationByID(Guid specializationID)
    {
        return await mediator.Send(new GetSpecializationByIDQuery(specializationID));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSpecialization([FromHeader] int PageNumber, [FromHeader] int PageSize)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new GetAllSpecializationQuery(paginationRequestHeader, Response));
    }

    [HttpGet]
    public async Task<IActionResult> SearchSpecialization([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] SearchSpecializationDTO searchSpecializationDTO)
    {
        PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
        return await mediator.Send(new SearchSpecializationQuery(paginationRequestHeader, searchSpecializationDTO, Response));
    }

}
