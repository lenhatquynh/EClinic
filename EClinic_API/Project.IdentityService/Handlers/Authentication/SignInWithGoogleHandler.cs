using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Response;
using Project.Common.Security;
using Project.Core.Authentication;
using Project.Core.Logger;
using Project.Core.Model;
using Project.IdentityService.Commands;
using Project.IdentityService.Data;
using Project.IdentityService.Dtos;
using Project.IdentityService.Protos;
using Project.IdentityService.Repository.RoleRepository;
using Project.IdentityService.Repository.UserRepository;
using System.Net.Http.Headers;

namespace Project.IdentityService.Handlers.Authentication
{
    public class SignInWithGoogleHandler : IRequestHandler<SignInWithGoogleCommand, ObjectResult>
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly ILogger<SignInHandler> logger;
        private readonly ProfileService.ProfileServiceClient profileClient;


        public SignInWithGoogleHandler(IConfiguration configuration, IUserRepository userRepository, IRoleRepository roleRepository, ILogger<SignInHandler> logger)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.logger = logger;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            profileClient = new ProfileService.ProfileServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(SignInWithGoogleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri($"https://www.googleapis.com/oauth2/v1/userinfo?{request.GoogleAccessToken}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.GoogleAccessToken);
                var result = await client.GetFromJsonAsync<GoogleUser>(client.BaseAddress);
                var checkEmail = await profileClient.EmailIsExistAsync(new CheckEmailRequest { Email = result.Email });
                if (checkEmail.IsExist)
                {
                    var userID = Guid.Parse(checkEmail.UserID);
                    var user = await userRepository.GetAsync(userID);
                    if (!user.Enabled)
                    {
                        return ApiResponse.BadRequest("User is not available");
                    }
                    var role = await roleRepository.GetAsync(role => role.RoleID == user.RoleID);
                    var tokenInformation = new JWTTokenInformation { Role = role.RoleName, UserID = user.UserID };
                    var tokenModel = TokenExtensions.GetToken(tokenInformation);
                    return ApiResponse.OK(tokenModel);
                }
                var pass = Cryptography.EncryptPassword(result.Id);
                var userCreate = new User { UserName = result.Email, PasswordHash = pass.Hash, PasswordSalt = pass.Salt, RoleID = RoleConstants.IDUser, CreatedAt = DateTime.Now };
                var res = await userRepository.CreateEntityAsync(userCreate);
                if (res == null)
                {
                    return ApiResponse.InternalServerError();
                }
                var response = await profileClient.CreateProfileAsync(new CreateProfileRequest
                {
                    UserID = res.UserID.ToString(),
                    Email = result.Email,
                    FirstName = result.Family_Name,
                    LastName = result.Given_Name,
                    Gender = false,
                    DateOfBirth = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow)

                });
                if (!response.IsSuccess)
                {
                    await userRepository.DeleteAsync(res);
                    return ApiResponse.InternalServerError();
                }
                var tokenInformations = new JWTTokenInformation { Role = RoleConstants.User, UserID = res.UserID };
                var tokenModels = TokenExtensions.GetToken(tokenInformations);
                return ApiResponse.OK(tokenModels);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
