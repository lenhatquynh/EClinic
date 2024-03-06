using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Authentication;
using Project.Core.Logger;
using Project.Core.Model;
using Project.IdentityService.Commands;
using Project.IdentityService.Repository.RoleRepository;
using Project.IdentityService.Repository.UserRepository;

namespace Project.IdentityService.Handlers.Authentication
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, ObjectResult>
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly ILogger<RefreshTokenHandler> logger;

        public RefreshTokenHandler(IUserRepository userRepository, IRoleRepository roleRepository, ILogger<RefreshTokenHandler> logger)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var IsValidate = TokenExtensions.IsValidationToken(request.RefreshToken);
                if (!IsValidate)
                {
                    return ApiResponse.Unauthorized("This token is not part of the system.");
                }
                var isExpires = TokenExtensions.CheckExpires(request.RefreshToken);
                if (isExpires)
                {
                    return ApiResponse.Unauthorized("Token has expired.");
                }
                var userID = TokenExtensions.GetID(request.RefreshToken);
                var user = await userRepository.GetAsync(userID);
                if (user == null)
                {
                    return ApiResponse.NotFound("User not found.");
                }
                if (!user.Enabled)
                {
                    return ApiResponse.BadRequest("User is not available");
                }
                var role = await roleRepository.GetAsync(role => role.RoleID == user.RoleID);
                var tokenInformation = new JWTTokenInformation { Role = role.RoleName, UserID = user.UserID };
                var tokenModel = TokenExtensions.GetToken(tokenInformation);
                return ApiResponse.OK(tokenModel);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
