using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.AWS;
using Project.Core.Logger;
using Project.ProfileService.Commands;
using Project.ProfileService.Data.Configurations;
using Project.ProfileService.Handlers.ProfileHandlers;
using Project.ProfileService.Repository.ProfileRepository;

namespace Project.ProfileService.Handlers.UserProfileHandlers
{
    public class DeleteUserProfileHandler : IRequestHandler<DeleteUserProfileCommands, ObjectResult>
    {
        private readonly IProfileRepository profileRepository;
        private readonly IAmazonS3Bucket s3Bucket;
        private readonly ILogger<DeleteProfileHandler> logger;

        public DeleteUserProfileHandler(IProfileRepository profileRepository, IAmazonS3Bucket s3Bucket, ILogger<DeleteProfileHandler> logger)
        {
            this.profileRepository = profileRepository;
            this.s3Bucket = s3Bucket;
            this.logger = logger;
        }
        public async Task<ObjectResult> Handle(DeleteUserProfileCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await profileRepository.GetUserProfileByIDAsync(request.ProfileID);
                if (profile == null)
                {
                    return ApiResponse.NotFound("Profile Not Found.");
                }
                if (profile.HealthProfile == null)
                {
                    return ApiResponse.NotFound("Profile Not Found.");
                }
                if (profile.HealthProfile.RelationshipID == ConstantsData.MyRelationshipID)
                {
                    return ApiResponse.BadRequest("Do not delete the main profile.");
                }
                var result = await profileRepository.DeleteAsync(profile);
                if (!result)
                {
                    throw new Exception("Delete Profile Error.");
                }
                if (!String.IsNullOrEmpty(profile.Avatar))
                {
                    await s3Bucket.DeleteFileAsync(profile.Avatar);
                }
                return ApiResponse.OK("Delete Profile Success");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
