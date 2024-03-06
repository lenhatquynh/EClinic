using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Response;
using Project.Core.Authentication;
using Project.Core.Caching.Attributes;
using Project.Core.Logger;
using Project.IdentityService.Queries;

namespace Project.IdentityService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [ApiVersion("1")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<RoleController> logger;
        public RoleController(IMediator mediator, ILogger<RoleController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        [Cache(1000)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await mediator.Send(new GetAllRoleQuery());
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
