using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ProfileService.Data.Configurations;
using Project.ProfileService.Dtos.UserProfile;
using Project.ProfileService.Protos;
using Project.ProfileService.Queries;
using Project.ProfileService.Repository.ProfileRepository;

namespace Project.ProfileService.Handlers.UserProfileHandlers
{
    public class GetUserProfileDetailByIDHandler : IRequestHandler<GetUserProfileDetailByIDQuery, ObjectResult>
    {
        private readonly IProfileRepository repository;
        private readonly ILogger<GetUserProfileDetailByIDHandler> logger;
        private readonly UserService.UserServiceClient client;
        private readonly IMapper mapper;
        public GetUserProfileDetailByIDHandler(IConfiguration configuration, IProfileRepository profileRepository, ILogger<GetUserProfileDetailByIDHandler> logger, IMapper mapper)
        {
            this.repository = profileRepository;
            this.logger = logger;
            this.mapper = mapper;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:IdentityServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new UserService.UserServiceClient(channel);
        }
        public async Task<ObjectResult> Handle(GetUserProfileDetailByIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userProfiles = await repository.GetProfilesAsync(request.UserID);
                if (userProfiles == null)
                {
                    return ApiResponse.NotFound("Profile Not Found.");
                }
                Data.Profile profile = userProfiles.SingleOrDefault(x => x.HealthProfile.RelationshipID == ConstantsData.MyRelationshipID);
                var userProfileDtos = mapper.Map<GetUserProfileDtos>(profile);
                var userRes = await client.GetUserAsync(new GetUserRequest { UserID = profile.UserID.ToString() });
                if (userRes == null)
                {
                    return ApiResponse.InternalServerError();
                }
                userProfileDtos.EnabledAccount = userRes.Enabled;
                return ApiResponse.OK<GetUserProfileDtos>(userProfileDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
