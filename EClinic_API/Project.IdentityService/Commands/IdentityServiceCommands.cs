using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.IdentityService.Dtos;

namespace Project.IdentityService.Commands
{
    public record SignInCommand(SignInDtos SignInDtos) : IRequest<ObjectResult>;
    public record SignInWithGoogleCommand(string GoogleAccessToken) : IRequest<ObjectResult>;
    public record SignUpCommand(SignUpDtos SignUpDtos) : IRequest<ObjectResult>;
    public record ConfirmSignUpCommand(ConfirmDataDtos ConfirmDataDtos) : IRequest<ObjectResult>;
    public record RefreshTokenCommand(string RefreshToken) : IRequest<ObjectResult>;
    public record ChangePasswordCommand(ChangePasswordDtos ChangePasswordDtos, string UserID) : IRequest<ObjectResult>;
    public record ResetPasswordCommand(ResetPasswordDTO ResetPasswordDTO) : IRequest<ObjectResult>;
    public record ConfirmResetPasswordCommand(ConfirmDataDtos ConfirmDataDtos) : IRequest<ObjectResult>;
    public record ChangeStatusCommand(Guid UserID) : IRequest<ObjectResult>;
    public record CreateUserCommand(string Email, string Role, bool Enabled) : IRequest<Guid>;
    public record UpdateUserCommand(Guid UserID, bool Enabled, string Email) : IRequest<bool>;
    public record ReSendCodeCommand(ResendCodeDtos ResendCodeDtos) : IRequest<ObjectResult>;
}
