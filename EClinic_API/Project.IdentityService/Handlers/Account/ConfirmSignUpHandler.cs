using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Response;
using Project.Core.Caching.Service;
using Project.Core.Logger;
using Project.IdentityService.Commands;
using Project.IdentityService.Data;
using Project.IdentityService.Dtos;
using Project.IdentityService.Protos;
using Project.IdentityService.Repository.UserRepository;

namespace Project.IdentityService.Handlers.Account
{
    public class ConfirmSignUpHandler : IRequestHandler<ConfirmSignUpCommand, ObjectResult>
    {
        private readonly IUserRepository userRepository;
        private readonly ProfileService.ProfileServiceClient client;
        private readonly ILogger<ConfirmSignUpHandler> logger;
        private readonly IResponseCacheService cacheService;

        public ConfirmSignUpHandler(IUserRepository userRepository, IConfiguration configuration, ILogger<ConfirmSignUpHandler> logger, IResponseCacheService cacheService)
        {
            this.userRepository = userRepository;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
            this.logger = logger;
            this.cacheService = cacheService;
        }

        public async Task<ObjectResult> Handle(ConfirmSignUpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await cacheService.GetCacheResponseAsync(request.ConfirmDataDtos.Key);
                var data = JsonConvert.DeserializeObject<DataCodeDtos>(res);
                if (data.Code != request.ConfirmDataDtos.Code)
                {
                    return ApiResponse.NotFound("Incorrect code");
                }
                User user = data.User;
                var result = await userRepository.CreateEntityAsync(user);
                if (result == null)
                {
                    return ApiResponse.InternalServerError();
                }
                CreateProfileRequest createProfile = data.CreateProfileRequest;
                createProfile.UserID = user.UserID.ToString();
                var response = await client.CreateProfileAsync(createProfile);
                if (!response.IsSuccess)
                {
                    await userRepository.DeleteAsync(user);
                    return ApiResponse.InternalServerError();
                }
                return ApiResponse.Created("Sign Up Success");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
