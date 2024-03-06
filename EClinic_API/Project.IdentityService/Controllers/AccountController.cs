using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Authentication;
using Project.Core.Logger;
using Project.IdentityService.Commands;
using Project.IdentityService.Dtos;
using Project.IdentityService.Queries;

namespace Project.IdentityService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [ApiVersion("1")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<AccountController> logger;
        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> GetStatisticsOverview()
        {
            try
            {
                return await mediator.Send(new GetStatisticsOverviewQuery());
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] SearchUserDtos searchUserDtos)
        {
            try
            {
                PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
                return await mediator.Send(new GetAllUserQuery(paginationRequestHeader, searchUserDtos, Response));
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDtos signUpDtos)
        {
            try
            {
                return await mediator.Send(new SignUpCommand(signUpDtos));
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmSignUp(ConfirmDataDtos ConfirmDataDtos)
        {
            try
            {
                return await mediator.Send(new ConfirmSignUpCommand(ConfirmDataDtos));
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                return await mediator.Send(new ResetPasswordCommand(resetPasswordDTO));
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmResetPassword(ConfirmDataDtos ConfirmDataDtos)
        {
            try
            {
                return await mediator.Send(new ConfirmResetPasswordCommand(ConfirmDataDtos));
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ResendCode(ResendCodeDtos ResendCodeDtos)
        {
            try
            {
                return await mediator.Send(new ReSendCodeCommand(ResendCodeDtos));
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDtos changePasswordDtos)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
                return await mediator.Send(new ChangePasswordCommand(changePasswordDtos, userId));
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
        [HttpPut]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> ChangeStatus(Guid UserID)
        {
            try
            {
                return await mediator.Send(new ChangeStatusCommand(UserID));
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }

    }
}
