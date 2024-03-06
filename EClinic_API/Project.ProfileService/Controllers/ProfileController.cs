using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Paging;
using Project.Core.Authentication;
using Project.ProfileService.Commands;
using Project.ProfileService.Dtos.DoctorProfile;
using Project.ProfileService.Dtos.EmployeeProfile;
using Project.ProfileService.Dtos.UserProfile;
using Project.ProfileService.Queries;

namespace Project.ProfileService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [ApiVersion("1")]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProfileController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SearchFamlyProfiles([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] string SearchText = "")
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            return await mediator.Send(new SearchFamilyProfileQuery(paginationRequestHeader, SearchText, Response, userId));
        }
        [HttpGet]
        public async Task<IActionResult> GetUserProfiles([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] string SearchText = "")
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetUserProfileQuery(paginationRequestHeader, SearchText, Response));
        }
        [HttpGet]
        public async Task<IActionResult> SearchDoctorProfiles([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] SearchDoctorDtos searchDoctor)
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new SearchDoctorProfileQuery(paginationRequestHeader, searchDoctor, Response));
        }
        [HttpGet]
        public async Task<IActionResult> GetDoctorProfiles([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] string SearchText = "")
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetDoctorProfileQuery(paginationRequestHeader, SearchText, Response));
        }
        [HttpGet]
        public async Task<IActionResult> GetAdminProfiles([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] string SearchText = "")
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetEmployeeProfileQuery(paginationRequestHeader, SearchText, Response, RoleConstants.Admin));
        }
        [HttpGet]
        public async Task<IActionResult> GetSupporterProfiles([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] string SearchText = "")
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetEmployeeProfileQuery(paginationRequestHeader, SearchText, Response, RoleConstants.Supporter));
        }
        [HttpGet]
        public async Task<IActionResult> GetExpertProfiles([FromHeader] int PageNumber, [FromHeader] int PageSize, [FromQuery] string SearchText = "")
        {
            PaginationRequestHeader paginationRequestHeader = new PaginationRequestHeader { PageSize = PageSize, PageNumber = PageNumber };
            return await mediator.Send(new GetEmployeeProfileQuery(paginationRequestHeader, SearchText, Response, RoleConstants.Expert));
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserMainProfilesByID()
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            return await mediator.Send(new GetUserMainProfilesByIDQuery(userId));
        }
        [HttpGet]
        public async Task<IActionResult> GetUserProfileDetailByID(Guid UserID)
        {
            return await mediator.Send(new GetUserProfileDetailByIDQuery(UserID));
        }
        [HttpGet]
        public async Task<IActionResult> GetUserProfilesByID(Guid UserID)
        {
            return await mediator.Send(new GetUserProfilesByIDQuery(UserID));
        }
        [HttpGet]
        public async Task<IActionResult> GetDoctorProfileByID(Guid UserID)
        {
            return await mediator.Send(new GetDoctorProfileByIDQuery(UserID));
        }
        [HttpGet]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Supporter, RoleConstants.Admin,RoleConstants.Expert })]
        public async Task<IActionResult> GetEmployeeProfileByID(Guid UserID)
        {
            return await mediator.Send(new GetEmployeeProfileByIDQuery(UserID));
        }
        [HttpGet]
        public async Task<IActionResult> GetSimpleProfile(Guid UserID)
        {
            return await mediator.Send(new GetSimpleProfileQuery(UserID));
        }
        [HttpPost]
        [CustomAuthorize(Authorities = new[] { RoleConstants.User })]
        public async Task<IActionResult> CreateUserProfile([FromForm] CreateUserProfileDtos createUserProfileDtos)
        {
            string userId = User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            return await mediator.Send(new CreateUserProfileCommands(createUserProfileDtos, userId));
        }
        [HttpPost]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> CreateDoctorProfile([FromForm] CreateDoctorProfileDtos createDoctorProfileDtos)
        {
            return await mediator.Send(new CreateDoctorProfileCommands(createDoctorProfileDtos));
        }
        [HttpPost]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> CreateSupporterProfile([FromForm] CreateEmployeeProfileDtos createSupporterProfileDtos)
        {
            return await mediator.Send(new CreateEmployeeProfileCommands(createSupporterProfileDtos, RoleConstants.Supporter));
        }
        [HttpPost]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> CreateAdminProfile([FromForm] CreateEmployeeProfileDtos createEmployeeProfileDtos)
        {
            return await mediator.Send(new CreateEmployeeProfileCommands(createEmployeeProfileDtos, RoleConstants.Admin));
        }
        [HttpPost]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> CreateExpertProfile([FromForm] CreateEmployeeProfileDtos createEmployeeProfileDtos)
        {
            return await mediator.Send(new CreateEmployeeProfileCommands(createEmployeeProfileDtos, RoleConstants.Expert));
        }
        [HttpPut]
        [CustomAuthorize(Authorities = new[] { RoleConstants.User })]
        public async Task<IActionResult> UpdateUserProfile([FromForm] UpdateUserProfileDtos updateUserProfile)
        {
            return await mediator.Send(new UpdateUserProfileCommands(updateUserProfile));
        }
        [HttpPut]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> UpdateDoctorProfile([FromForm] UpdateDoctorProfileDtos updateDoctorProfileDtos)
        {
            return await mediator.Send(new UpdateDoctorProfileCommands(updateDoctorProfileDtos));
        }
        [HttpPut]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> UpdateSupporterProfile([FromForm] UpdateEmployeeProfileDtos updateSupporterProfileDtos)
        {
            return await mediator.Send(new UpdateEmployeeProfileCommands(updateSupporterProfileDtos));
        }
        [HttpPut]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> UpdateAdminProfile([FromForm] UpdateEmployeeProfileDtos updateEmployeeProfileDtos)
        {
            return await mediator.Send(new UpdateEmployeeProfileCommands(updateEmployeeProfileDtos));
        }
        [HttpPut]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> UpdateExpertProfile([FromForm] UpdateEmployeeProfileDtos updateEmployeeProfileDtos)
        {
            return await mediator.Send(new UpdateEmployeeProfileCommands(updateEmployeeProfileDtos));
        }
        [HttpDelete]
        [CustomAuthorize(Authorities = new[] { RoleConstants.User })]
        public async Task<IActionResult> DeleteUserProfile(Guid ProfileID)
        {
            return await mediator.Send(new DeleteUserProfileCommands(ProfileID));
        }
        [HttpDelete]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> DeleteDoctorProfile(Guid ProfileID)
        {
            return await mediator.Send(new DeleteProfileCommands(ProfileID));
        }
        [HttpDelete]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> DeleteSupporterProfile(Guid ProfileID)
        {
            return await mediator.Send(new DeleteProfileCommands(ProfileID));
        }
        [HttpDelete]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> DeleteAdminProfile(Guid ProfileID)
        {
            return await mediator.Send(new DeleteProfileCommands(ProfileID));
        }
        [HttpDelete]
        [CustomAuthorize(Authorities = new[] { RoleConstants.Admin })]
        public async Task<IActionResult> DeleteExpertProfile(Guid ProfileID)
        {
            return await mediator.Send(new DeleteProfileCommands(ProfileID));
        }
    }
}
