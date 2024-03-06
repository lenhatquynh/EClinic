using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.ProfileService.Queries;

namespace Project.ProfileService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [ApiVersion("1")]
    public class OtherController : ControllerBase
    {
        private readonly IMediator mediator;

        public OtherController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetBloodTypes()
        {
            return await mediator.Send(new GetBloodTypesQuery());
        }
    }
}
