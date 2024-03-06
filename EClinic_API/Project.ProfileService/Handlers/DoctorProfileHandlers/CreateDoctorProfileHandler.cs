using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Enum;
using Project.Common.Response;
using Project.Core.AWS;
using Project.Core.Logger;
using Project.ProfileService.Commands;
using Project.ProfileService.Data;
using Project.ProfileService.Handlers.UserProfileHandlers;
using Project.ProfileService.Protos;
using Project.ProfileService.Repository.DoctorProfileRepository;
using Project.ProfileService.Repository.ProfileRepository;

namespace Project.ProfileService.Handlers.DoctorProfileHandlers
{
    public class CreateDoctorProfileHandler : IRequestHandler<CreateDoctorProfileCommands, ObjectResult>
    {
        private readonly IProfileRepository profileRepository;
        private readonly IDoctorProfileRepository doctorProfileRepository;
        private readonly IAmazonS3Bucket s3Bucket;
        private readonly ILogger<CreateUserProfileHandler> logger;
        private readonly UserService.UserServiceClient client;
        private readonly ServiceInformationService.ServiceInformationServiceClient serviceClient;

        public CreateDoctorProfileHandler(IConfiguration configuration, IProfileRepository profileRepository, IDoctorProfileRepository doctorProfileRepository, IAmazonS3Bucket s3Bucket, ILogger<CreateUserProfileHandler> logger)
        {
            this.profileRepository = profileRepository;
            this.doctorProfileRepository = doctorProfileRepository;
            this.s3Bucket = s3Bucket;
            this.logger = logger;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:IdentityServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            GrpcChannel channel2 = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ServiceInformationServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new UserService.UserServiceClient(channel);
            serviceClient = new ServiceInformationService.ServiceInformationServiceClient(channel2);
        }

        public async Task<ObjectResult> Handle(CreateDoctorProfileCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await serviceClient.CheckSpecializationAsync(new GetSpecializationRequest { SpecializationID = request.CreateDoctorProfileDtos.SpecializationID.ToString() });
                if(res == null)
                {
                    return ApiResponse.NotFound("Specialization Not Found");
                }
                var emailExist = await profileRepository.AnyAsync(x => x.Email == request.CreateDoctorProfileDtos.Email);
                if (emailExist)
                {
                    return ApiResponse.BadRequest("Email is exist");
                }
                var profile = new Data.Profile
                {
                    UserID = Guid.Empty,
                    Address = request.CreateDoctorProfileDtos.Address,
                    DateOfBirth = request.CreateDoctorProfileDtos.DateOfBirth,
                    FirstName = request.CreateDoctorProfileDtos.FirstName,
                    LastName = request.CreateDoctorProfileDtos.LastName,
                    Email = request.CreateDoctorProfileDtos.Email,
                    Gender = request.CreateDoctorProfileDtos.Gender,
                    Phone = request.CreateDoctorProfileDtos.Phone
                };
                if (request.CreateDoctorProfileDtos.Avatar != null)
                {
                    profile.Avatar = await s3Bucket.UploadFileAsync(request.CreateDoctorProfileDtos.Avatar, FileType.Image);
                }
                else
                {
                    profile.Avatar = null;
                }
                var userRes = await client.CreateUserAsync(new CreateUserRequest { Email = profile.Email, Role = RoleConstants.Doctor, Enabled = request.CreateDoctorProfileDtos.EnabledAccount });
                if (!userRes.IsSuccess)
                {
                    return ApiResponse.InternalServerError();
                }
                profile.UserID = Guid.Parse(userRes.UserID);
                var result = await profileRepository.CreateEntityAsync(profile);
                if (result == null)
                {
                    throw new Exception("Create Profile Error");
                }

                var doctor = new DoctorProfile
                {
                    ProfileID = result.ProfileID,
                    Description = request.CreateDoctorProfileDtos.Description,
                    SpecializationID = request.CreateDoctorProfileDtos.SpecializationID,
                    Title = request.CreateDoctorProfileDtos.Title,
                    Price = request.CreateDoctorProfileDtos.Price,
                    WorkStart = request.CreateDoctorProfileDtos.WorkStart,
                    Content = request.CreateDoctorProfileDtos.Content,
                    IsActive = request.CreateDoctorProfileDtos.IsActive
                };
                var doctorResult = await doctorProfileRepository.CreateAsync(doctor);
                if (!doctorResult)
                {
                    await profileRepository.DeleteAsync(profile);
                    throw new Exception("Create Doctor Profile Error.");
                }
                return ApiResponse.Created<Guid>(doctor.ProfileID);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
