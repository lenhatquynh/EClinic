using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Enum;
using Project.Common.Response;
using Project.Core.AWS;
using Project.Core.Logger;
using Project.ProfileService.Commands;
using Project.ProfileService.Data;
using Project.ProfileService.Data.Configurations;
using Project.ProfileService.Repository.HealthProfileRepository;
using Project.ProfileService.Repository.ProfileRepository;

namespace Project.ProfileService.Handlers.UserProfileHandlers
{
    public class CreateUserProfileHandler : IRequestHandler<CreateUserProfileCommands, ObjectResult>
    {
        private readonly IProfileRepository profileRepository;
        private readonly IHealthProfileRepository healthProfileRepository;
        private readonly IAmazonS3Bucket s3Bucket;
        private readonly ILogger<CreateUserProfileHandler> logger;

        public CreateUserProfileHandler(IProfileRepository profileRepository, IHealthProfileRepository healthProfileRepository, IAmazonS3Bucket s3Bucket, ILogger<CreateUserProfileHandler> logger)
        {
            this.profileRepository = profileRepository;
            this.healthProfileRepository = healthProfileRepository;
            this.s3Bucket = s3Bucket;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(CreateUserProfileCommands request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.CreateUserProfileDtos.RelationshipID == Guid.Empty) { return ApiResponse.BadRequest("RelationshipID not null."); }
                var emailExist = await profileRepository.AnyAsync(x => x.Email == request.CreateUserProfileDtos.Email);
                if (emailExist)
                {
                    return ApiResponse.BadRequest("Email is exist");
                }
                var profile = new Profile
                {
                    UserID = Guid.Parse(request.UserID),
                    Address = request.CreateUserProfileDtos.Address,
                    DateOfBirth = request.CreateUserProfileDtos.DateOfBirth,
                    FirstName = request.CreateUserProfileDtos.FirstName,
                    LastName = request.CreateUserProfileDtos.LastName,
                    Email = request.CreateUserProfileDtos.Email,
                    Gender = request.CreateUserProfileDtos.Gender,
                    Phone = request.CreateUserProfileDtos.Phone
                };
                if (request.CreateUserProfileDtos.Avatar != null)
                {
                    profile.Avatar = await s3Bucket.UploadFileAsync(request.CreateUserProfileDtos.Avatar, FileType.Image);
                }
                else
                {
                    profile.Avatar = await s3Bucket.GetFileAsync(ConstantsData.DefaultAvatarKey);
                }
                var result = await profileRepository.CreateEntityAsync(profile);
                if (result == null)
                {
                    throw new Exception("Create Profile Error");
                }
                var health = new HealthProfile
                {
                    ProfileID = result.ProfileID,
                    BloodType = request.CreateUserProfileDtos.BloodType,
                    Height = request.CreateUserProfileDtos.Height,
                    Weight = request.CreateUserProfileDtos.Weight,
                    RelationshipID = request.CreateUserProfileDtos.RelationshipID
                };
                var healthResult = await healthProfileRepository.CreateAsync(health);
                if (!healthResult)
                {
                    await profileRepository.DeleteAsync(profile);
                    throw new Exception("Create Health Profile Error.");
                }
                return ApiResponse.Created("CreateSuccess");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
