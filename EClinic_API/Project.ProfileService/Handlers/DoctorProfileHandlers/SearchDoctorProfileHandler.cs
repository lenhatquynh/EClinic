using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Constants;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ProfileService.Dtos.DoctorProfile;
using Project.ProfileService.Protos;
using Project.ProfileService.Queries;
using Project.ProfileService.Repository.ProfileRepository;

namespace Project.ProfileService.Handlers.DoctorProfileHandlers
{
    public class SearchDoctorProfileHandler : IRequestHandler<SearchDoctorProfileQuery, ObjectResult>
    {
        private readonly IProfileRepository profileRepository;
        private readonly ILogger<SearchDoctorProfileHandler> logger;
        private readonly UserService.UserServiceClient client;
        private readonly IMapper mapper;
        private readonly ServiceInformationService.ServiceInformationServiceClient serviceClient;

        public SearchDoctorProfileHandler(IConfiguration configuration, IProfileRepository profileRepository, ILogger<SearchDoctorProfileHandler> logger, IMapper mapper)
        {
            this.profileRepository = profileRepository;
            this.logger = logger;
            this.mapper = mapper;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:IdentityServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            GrpcChannel channel2 = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ServiceInformationServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new UserService.UserServiceClient(channel);
            serviceClient = new ServiceInformationService.ServiceInformationServiceClient(channel2);
        }

        public async Task<ObjectResult> Handle(SearchDoctorProfileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await client.GetAllUserWithRoleAsync(new GetAllUserWithRoleRequest { Role = RoleConstants.Doctor });
                if (res == null)
                {
                    throw new Exception("Get User Error");
                }
                var ListUser = res.User.ToList();
                List<Guid> listID = ListUser.Select(s => Guid.Parse(s.UserID)).ToList();
                var pagination = await profileRepository.SearchDoctorProfilesAsync(listID, request.PaginationRequestHeader, request.SearchDoctor);
                request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination.PaginationResponseHeader));
                var profileDtos = mapper.Map<List<GetDoctorProfileDtos>>(pagination.PaginationData);
                var listSpecializationID = profileDtos.Select(s => s.Specialization.SpecializationID.ToString()).ToList();
                GetAllSpecializationRequest req = new GetAllSpecializationRequest();
                req.SpecializationIDs.AddRange(listSpecializationID);
                var serviceRes = await serviceClient.GetAllSpecializationAsync(req);
                if (serviceRes == null)
                {
                    return ApiResponse.InternalServerError();
                }
                var ListService = serviceRes.Specialization.ToList();
                foreach (var item in profileDtos)
                {
                    var user = ListUser.SingleOrDefault(x => item.UserID == Guid.Parse(x.UserID));
                    item.EnabledAccount = user.Enabled;
                    var spe = ListService.FirstOrDefault(x => item.Specialization.SpecializationID == Guid.Parse(x.SpecializationID));
                    item.Specialization.SpecializationName = spe.SpecializationName;
                }
                return ApiResponse.OK<List<GetDoctorProfileDtos>>(profileDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
