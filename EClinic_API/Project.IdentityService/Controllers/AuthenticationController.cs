using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.IdentityService.Commands;
using Project.IdentityService.Dtos;

namespace Project.IdentityService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [ApiVersion("1")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<AuthenticationController> logger;

        public AuthenticationController(IMediator mediator, ILogger<AuthenticationController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDtos signInDtos)
        {
            try
            {
                return await mediator.Send(new SignInCommand(signInDtos));
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> SignInWithGoogle([FromBody] string GoogleAccessToken)
        {
            try
            {
                return await mediator.Send(new SignInWithGoogleCommand(GoogleAccessToken));
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
        [HttpGet]
        public async Task<IActionResult> RefreshToken([FromHeader] string RefreshToken)
        {
            try
            {
                return await mediator.Send(new RefreshTokenCommand(RefreshToken));
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            } 
        }
    }
}
