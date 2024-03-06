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
using Project.ProfileService.Repository.DoctorProfileRepository;
using Project.ProfileService.Repository.ProfileRepository;
using Project.ProfileServices.Events;

namespace Project.ProfileService.Handlers.DoctorProfileHandlers
{
    public class UpdateDoctorProfileHandler : IRequestHandler<UpdateDoctorProfileCommands, ObjectResult>
    {
        private readonly IProfileRepository profileRepository;
        private readonly IDoctorProfileRepository doctorProfileRepository;
        private readonly IAmazonS3Bucket s3Bucket;
        private readonly ILogger<UpdateDoctorProfileHandler> logger;
        private readonly IBus bus;
        private readonly ServiceInformationService.ServiceInformationServiceClient serviceClient;
        private readonly UserService.UserServiceClient client;
        public UpdateDoctorProfileHandler(IConfiguration configuration, IProfileRepository profileRepository, IDoctorProfileRepository doctorProfileRepository, IAmazonS3Bucket s3Bucket, ILogger<UpdateDoctorProfileHandler> logger, IBus bus)
        {
            this.profileRepository = profileRepository;
            this.doctorProfileRepository = doctorProfileRepository;
            this.s3Bucket = s3Bucket;
            this.logger = logger;
            this.bus = bus;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:IdentityServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            GrpcChannel channel2 = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ServiceInformationServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new UserService.UserServiceClient(channel);
            serviceClient = new ServiceInformationService.ServiceInformationServiceClient(channel2);
        }

        public async Task<ObjectResult> Handle(UpdateDoctorProfileCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await profileRepository.GetAsync(request.UpdateDoctorProfileDtos.ProfileID);
                if (profile == null)
                {
                    return ApiResponse.NotFound("Profile Not Found.");
                }
                var emailExist = await profileRepository.AnyAsync(x => x.Email == request.UpdateDoctorProfileDtos.Email && profile.Email != x.Email);
                if (emailExist)
                {
                    return ApiResponse.BadRequest("Email is exist");
                }
                var res = await serviceClient.CheckSpecializationAsync(new GetSpecializationRequest { SpecializationID = request.UpdateDoctorProfileDtos.SpecializationID.ToString() });
                if (res == null)
                {
                    return ApiResponse.NotFound("Specialization Not Found");
                }
                var profileDtos = request.UpdateDoctorProfileDtos;
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
                var doctorProfile = await doctorProfileRepository.GetAsync(profile.ProfileID);
                if (doctorProfile == null)
                {
                    return ApiResponse.NotFound("Profile Not Found.");
                }
                doctorProfile.WorkStart = profileDtos.WorkStart;
                doctorProfile.WorkEnd = profileDtos.WorkEnd;
                doctorProfile.Content = profileDtos.Content;
                doctorProfile.Description = profileDtos.Description;
                doctorProfile.Title = profileDtos.Title;
                doctorProfile.IsActive = profileDtos.IsActive;
                doctorProfile.SpecializationID = profileDtos.SpecializationID;
                var updateDoctorResult = await doctorProfileRepository.UpdateAsync(doctorProfile);
                if (!updateDoctorResult)
                {
                    throw new Exception("Update Doctor Profile Error");
                }
                var updateProfileResult = await profileRepository.UpdateAsync(profile);
                if (!updateProfileResult)
                {
                    throw new Exception("Update Profile Error");
                }
                var userRes = await client.UpdateUserAsync(new UpdateUserRequest { UserID = profile.UserID.ToString(), Enabled = profileDtos.EnabledAccount ,Email = profile.Email});
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
                }, ExchangeConstants.ForumService);
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
