using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.ProfileService.Dtos.DoctorProfile;
using Project.ProfileService.Dtos.EmployeeProfile;
using Project.ProfileService.Dtos.UserProfile;

namespace Project.ProfileService.Commands
{
    public record CreateUserProfileCommands(CreateUserProfileDtos CreateUserProfileDtos,string UserID) : IRequest<ObjectResult>;
    public record CreateDoctorProfileCommands(CreateDoctorProfileDtos CreateDoctorProfileDtos) : IRequest<ObjectResult>;
    public record CreateEmployeeProfileCommands(CreateEmployeeProfileDtos CreateEmployeeProfileDtos, string Role) : IRequest<ObjectResult>;
    public record UpdateDoctorProfileCommands(UpdateDoctorProfileDtos UpdateDoctorProfileDtos) : IRequest<ObjectResult>;
    public record UpdateEmployeeProfileCommands(UpdateEmployeeProfileDtos UpdateEmployeeProfileDtos) : IRequest<ObjectResult>;
    public record UpdateUserProfileCommands(UpdateUserProfileDtos UpdateUserProfileDtos) : IRequest<ObjectResult>;
    public record DeleteUserProfileCommands(Guid ProfileID) : IRequest<ObjectResult>;
    public record DeleteProfileCommands(Guid ProfileID) : IRequest<ObjectResult>;

}