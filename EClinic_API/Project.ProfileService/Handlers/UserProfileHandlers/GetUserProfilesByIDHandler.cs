using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ProfileService.Dtos.UserProfile;
using Project.ProfileService.Queries;
using Project.ProfileService.Repository.ProfileRepository;
using Project.ProfileService.Repository.RelationshipRepository;

namespace Project.ProfileService.Handlers.UserProfileHandlers
{
    public class GetUserProfilesByIDHandler : IRequestHandler<GetUserProfilesByIDQuery, ObjectResult>
    {
        private readonly ILogger<GetUserProfilesByIDHandler> logger;
        private readonly IProfileRepository repository;
        private readonly IMapper mapper;
        private readonly IRelationshipRepository relationshipRepository;

        public GetUserProfilesByIDHandler(ILogger<GetUserProfilesByIDHandler> logger, IProfileRepository repository, IMapper mapper, IRelationshipRepository relationshipRepository)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
            this.relationshipRepository = relationshipRepository;
        }

        public async Task<ObjectResult> Handle(GetUserProfilesByIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userProfiles = await repository.GetProfilesAsync(request.UserID);
                if (userProfiles == null)
                {
                    return ApiResponse.NotFound("Profile Not Found.");
                }
                var userProfileDtos = mapper.Map<List<UserProfileDtos>>(userProfiles);
                foreach (var profile in userProfileDtos)
                {
                    var relationship = await relationshipRepository.GetAsync(profile.RelationshipID);
                    profile.RelationshipName = relationship.RelationshipName;
                }
                return ApiResponse.OK<List<UserProfileDtos>>(userProfileDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
