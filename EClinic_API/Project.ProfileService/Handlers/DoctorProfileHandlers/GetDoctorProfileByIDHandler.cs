using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ProfileService.Dtos.DoctorProfile;
using Project.ProfileService.Protos;
using Project.ProfileService.Queries;
using Project.ProfileService.Repository.ProfileRepository;

namespace Project.ProfileService.Handlers.DoctorProfileHandlers
{
    public class GetDoctorProfileByIDHandler : IRequestHandler<GetDoctorProfileByIDQuery, ObjectResult>
    {
        private readonly ILogger<GetDoctorProfileByIDHandler> logger;
        private readonly IProfileRepository repository;
        private readonly IMapper mapper;
        private readonly UserService.UserServiceClient client;
        private readonly ServiceInformationService.ServiceInformationServiceClient serviceClient;

        public GetDoctorProfileByIDHandler(IConfiguration configuration, ILogger<GetDoctorProfileByIDHandler> logger, IProfileRepository repository, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:IdentityServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            GrpcChannel channel2 = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ServiceInformationServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new UserService.UserServiceClient(channel);
            serviceClient = new ServiceInformationService.ServiceInformationServiceClient(channel2);
        }

        public async Task<ObjectResult> Handle(GetDoctorProfileByIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var doctorProfiles = await repository.GetDoctorProfileAsync(request.UserID);
                if (doctorProfiles == null)
                {
                    return ApiResponse.NotFound("Profile Not Found.");
                }
                var userRes = await client.GetUserAsync(new GetUserRequest { UserID = doctorProfiles.UserID.ToString() });
                if (userRes == null)
                {
                    return ApiResponse.InternalServerError();
                }
                var serviceRes = await serviceClient.GetSpecializationAsync(new GetSpecializationRequest { SpecializationID = doctorProfiles.DoctorProfile.SpecializationID.ToString() });
                if (serviceRes == null)
                {
                    return ApiResponse.InternalServerError();
                }
                var doctorProfileDtos = mapper.Map<DoctorProfileDtos>(doctorProfiles);
                doctorProfileDtos.EnabledAccount = userRes.Enabled;
                doctorProfileDtos.Specialization = new SpecializationDtos
                {
                    SpecializationName = serviceRes.SpecializationName,
                    SpecializationID = doctorProfiles.DoctorProfile.SpecializationID
                };
                return ApiResponse.OK<DoctorProfileDtos>(doctorProfileDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
