using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Enum;
using Project.Common.Response;
using Project.Core.AWS;
using Project.Core.Logger;
using Project.Core.RabbitMQ;
using Project.ProfileService.Commands;
using Project.ProfileService.Data.Configurations;
using Project.ProfileService.Repository.HealthProfileRepository;
using Project.ProfileService.Repository.ProfileRepository;
using Project.ProfileServices.Events;

namespace Project.ProfileService.Handlers.UserProfileHandlers
{
    public class UpdateUserProfileHandler : IRequestHandler<UpdateUserProfileCommands, ObjectResult>
    {
        private readonly IProfileRepository profileRepository;
        private readonly IHealthProfileRepository healthProfileRepository;
        private readonly ILogger<UpdateUserProfileHandler> logger;
        private readonly IAmazonS3Bucket s3Bucket;
        private readonly IBus bus;

        public UpdateUserProfileHandler(IProfileRepository profileRepository, IHealthProfileRepository healthProfileRepository, ILogger<UpdateUserProfileHandler> logger, IAmazonS3Bucket s3Bucket, IBus bus)
        {
            this.profileRepository = profileRepository;
            this.healthProfileRepository = healthProfileRepository;
            this.logger = logger;
            this.s3Bucket = s3Bucket;
            this.bus = bus;
        }

        public async Task<ObjectResult> Handle(UpdateUserProfileCommands request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UpdateUserProfileDtos.RelationshipID == Guid.Empty) { return ApiResponse.BadRequest("RelationshipID not null."); }
                var profile = await profileRepository.GetAsync(request.UpdateUserProfileDtos.ProfileID);
                if (profile == null)
                {
                    return ApiResponse.NotFound("Profile Not Found.");
                }
                var emailExist = await profileRepository.AnyAsync(x => x.Email == request.UpdateUserProfileDtos.Email && profile.Email != x.Email);
                if (emailExist)
                {
                    return ApiResponse.BadRequest("Email is exist");
                }
                var profileDtos = request.UpdateUserProfileDtos;
                profile.FirstName = profileDtos.FirstName;
                profile.LastName = profileDtos.LastName;
                profile.Email = profileDtos.Email;
                profile.DateOfBirth = profileDtos.DateOfBirth;
                profile.Gender = profileDtos.Gender;
                profile.Address = profileDtos.Address;
                profile.Phone = profileDtos.Phone;
                if (profileDtos.Avatar != null)
                {
                    profile.Avatar = await s3Bucket.UploadFileAsync(profileDtos.Avatar, FileType.Image);
                }
                var healthProfile = await healthProfileRepository.GetAsync(profile.ProfileID);
                if (healthProfile == null)
                {
                    return ApiResponse.NotFound("Profile Not Found.");
                }
                if (healthProfile.RelationshipID != ConstantsData.MyRelationshipID)
                {
                    healthProfile.RelationshipID = profileDtos.RelationshipID;
                }
                if(healthProfile.RelationshipID == ConstantsData.MyRelationshipID)
                {
                    await bus.SendMessageWithExchangeName<UpdateProfileEvents>(new UpdateProfileEvents
                    {
                        UserID = profile.UserID,
                        Avatar = profile.Avatar,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName
                    }, ExchangeConstants.ForumService);
                    await bus.SendMessageWithExchangeName<UpdateProfileEvents>(new UpdateProfileEvents
                    {
                        UserID = profile.UserID,
                        Avatar = profile.Avatar,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName
                    }, ExchangeConstants.BlogsService);
                }
                healthProfile.Height = profileDtos.Height;
                healthProfile.Weight = profileDtos.Weight;
                healthProfile.BloodType = profileDtos.BloodType;
                var updateHealthResult = await healthProfileRepository.UpdateAsync(healthProfile);
                if (!updateHealthResult)
                {
                    throw new Exception("Update Doctor Profile Error");
                }
                var deleteProfileResult = await profileRepository.UpdateAsync(profile);
                if (!deleteProfileResult)
                {
                    throw new Exception("Update Profile Error");
                }
                return ApiResponse.OK("Update Profile Success");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
