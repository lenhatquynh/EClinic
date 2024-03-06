using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.ProfileService.Queries;

namespace Project.ProfileService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [ApiVersion("1")]
    public class RelationshipController : ControllerBase
    {
        private readonly IMediator mediator;

        public RelationshipController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllRelationship()
        {
            return await mediator.Send(new GetAllRelationshipQuery());
        }
    }
}
