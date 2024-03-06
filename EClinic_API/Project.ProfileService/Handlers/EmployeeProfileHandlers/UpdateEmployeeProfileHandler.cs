using Grpc.Net.Client;
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
using Project.ProfileService.Events;
using Project.ProfileService.Protos;
using Project.ProfileService.Repository.EmployeeProfileRepository;
using Project.ProfileService.Repository.ProfileRepository;
using Project.ProfileServices.Events;

namespace Project.ProfileService.Handlers.EmployeeProfileHandlers
{
    public class UpdateEmployeeProfileHandler : IRequestHandler<UpdateEmployeeProfileCommands, ObjectResult>
    {
        private readonly IProfileRepository profileRepository;
        private readonly IEmployeeProfilesRepository employeeProfilesRepository;
        private readonly ILogger<UpdateEmployeeProfileHandler> logger;
        private readonly IAmazonS3Bucket s3Bucket;
        private readonly IBus bus;
        private readonly UserService.UserServiceClient client;

        public UpdateEmployeeProfileHandler(IConfiguration configuration, IProfileRepository profileRepository, IEmployeeProfilesRepository employeeProfilesRepository, ILogger<UpdateEmployeeProfileHandler> logger, IAmazonS3Bucket s3Bucket, IBus bus)
        {
            this.profileRepository = profileRepository;
            this.employeeProfilesRepository = employeeProfilesRepository;
            this.logger = logger;
            this.s3Bucket = s3Bucket;
            this.bus = bus;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:IdentityServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new UserService.UserServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(UpdateEmployeeProfileCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await profileRepository.GetAsync(request.UpdateEmployeeProfileDtos.ProfileID);
                if (profile == null)
                {
                    return ApiResponse.NotFound("Profile Not Found.");
                }
                var emailExist = await profileRepository.AnyAsync(x => x.Email == request.UpdateEmployeeProfileDtos.Email && profile.Email != x.Email);
                if (emailExist)
                {
                    return ApiResponse.BadRequest("Email is exist");
                }
                var profileDtos = request.UpdateEmployeeProfileDtos;
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
                var supporteProfile = await employeeProfilesRepository.GetAsync(profile.ProfileID);
                if (supporteProfile == null)
                {
                    return ApiResponse.NotFound("Profile Not Found.");
                }
                supporteProfile.WorkStart = profileDtos.WorkStart;
                supporteProfile.WorkEnd = profileDtos.WorkEnd;
                supporteProfile.Description = profileDtos.Description;
                var updateSupporteResult = await employeeProfilesRepository.UpdateAsync(supporteProfile);
                if (!updateSupporteResult)
                {
                    throw new Exception("Update Doctor Profile Error");
                }
                var updateProfileResult = await profileRepository.UpdateAsync(profile);
                if (!updateProfileResult)
                {
                    throw new Exception("Update Profile Error");
                }
                var userRes = await client.UpdateUserAsync(new UpdateUserRequest { UserID = profile.UserID.ToString(), Enabled = profileDtos.EnabledAccount });
                if (!userRes.IsSuccess)
                {
                    return ApiResponse.InternalServerError();
                }
                await bus.SendMessageWithExchangeName<UpdateProfileEvents>(new UpdateProfileEvents
                {
                    UserID = profile.UserID,
                    Avatar = profile.Avatar,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName
                }, ExchangeConstants.BlogsService);
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
