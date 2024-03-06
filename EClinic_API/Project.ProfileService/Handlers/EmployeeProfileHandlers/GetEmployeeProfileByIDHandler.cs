using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ProfileService.Dtos.EmployeeProfile;
using Project.ProfileService.Protos;
using Project.ProfileService.Queries;
using Project.ProfileService.Repository.ProfileRepository;

namespace Project.ProfileService.Handlers.EmployeeProfileHandlers
{
    public class GetEmployeeProfileByIDHandler : IRequestHandler<GetEmployeeProfileByIDQuery, ObjectResult>
    {
        private readonly ILogger<GetEmployeeProfileByIDHandler> logger;
        private readonly IProfileRepository repository;
        private readonly IMapper mapper;
        private readonly UserService.UserServiceClient client;
        public GetEmployeeProfileByIDHandler(IConfiguration configuration, ILogger<GetEmployeeProfileByIDHandler> logger, IProfileRepository repository, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:IdentityServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new UserService.UserServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(GetEmployeeProfileByIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var supporterProfiles = await repository.GetEmployeeProfileAsync(request.UserID);
                if (supporterProfiles == null)
                {
                    return ApiResponse.NotFound("Profile Not Found.");
                }
                var userRes = await client.GetUserAsync(new GetUserRequest { UserID = supporterProfiles.UserID.ToString() });
                if (userRes == null)
                {
                    throw new Exception("Get Enable User Error");
                }
                var supporteProfileDtos = mapper.Map<EmployeeProfileDtos>(supporterProfiles);
                supporteProfileDtos.EnabledAccount = userRes.Enabled;
                return ApiResponse.OK<EmployeeProfileDtos>(supporteProfileDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
