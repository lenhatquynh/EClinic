using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.CommunicateService.Queries;

namespace Project.CommunicateService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [ApiVersion("1")]
    public class RoomTypesController : ControllerBase
    {
        private readonly IMediator mediator;

        public RoomTypesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRoomType()
        {
            return await mediator.Send(new GetAllRoomTypeQuery());
        }
    }
}
