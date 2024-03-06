using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ProfileService.Data.Configurations;
using Project.ProfileService.Dtos.Profile;
using Project.ProfileService.Queries;
using Project.ProfileService.Repository.ProfileRepository;
using Profile = Project.ProfileService.Data.Profile;

namespace Project.ProfileService.Handlers.ProfileHandlers
{
    public class GetSimpleProfileHandler : IRequestHandler<GetSimpleProfileQuery, ObjectResult>
    {
        private readonly IProfileRepository profileRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GetSimpleProfileHandler> logger;

        public GetSimpleProfileHandler(IProfileRepository profileRepository, IMapper mapper, ILogger<GetSimpleProfileHandler> logger)
        {
            this.profileRepository = profileRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetSimpleProfileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var profiles = await profileRepository.GetProfilesAsync(request.UserID);
                var profile = new Profile();
                if (profiles == null)
                {
                    return ApiResponse.NotFound("Profile Not Found.");
                }
                if (profiles.Count == 1)
                {
                    profile = profiles[0];
                }
                else
                {
                    profile = profiles.SingleOrDefault(x => x.HealthProfile.RelationshipID == ConstantsData.MyRelationshipID);
                }
                var simpleProfile = mapper.Map<SimpleProfileDtos>(profile);
                return ApiResponse.OK<SimpleProfileDtos>(simpleProfile);

            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
