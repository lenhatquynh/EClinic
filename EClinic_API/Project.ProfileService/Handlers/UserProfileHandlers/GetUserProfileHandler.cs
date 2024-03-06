using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Constants;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ProfileService.Dtos.UserProfile;
using Project.ProfileService.Protos;
using Project.ProfileService.Queries;
using Project.ProfileService.Repository.ProfileRepository;

namespace Project.ProfileService.Handlers.UserProfileHandlers
{
    public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, ObjectResult>
    {
        private readonly IProfileRepository profileRepository;
        private readonly ILogger<GetUserProfileHandler> logger;
        private readonly UserService.UserServiceClient client;
        private readonly IMapper mapper;
        public GetUserProfileHandler(IConfiguration configuration, IProfileRepository profileRepository, ILogger<GetUserProfileHandler> logger, IMapper mapper)
        {
            this.profileRepository = profileRepository;
            this.logger = logger;
            this.mapper = mapper;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:IdentityServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new UserService.UserServiceClient(channel);
        }
        public async Task<ObjectResult> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await client.GetAllUserWithRoleAsync(new GetAllUserWithRoleRequest { Role = RoleConstants.User });
                if (res == null)
                {
                    throw new Exception("Get User Error");
                }
                var ListUser = res.User.ToList();
                List<Guid> listID = ListUser.Select(s => Guid.Parse(s.UserID)).ToList();
                var pagination = await profileRepository.GetUserProfilesAsync(listID, request.PaginationRequestHeader, request.SearchText);
                request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination.PaginationResponseHeader));
                var profileDtos = mapper.Map<List<GetUserProfileDtos>>(pagination.PaginationData);
                foreach(var item in  profileDtos)
                {
                    var user = ListUser.SingleOrDefault(x => item.UserID == Guid.Parse(x.UserID));
                    item.EnabledAccount = user.Enabled;
                }
                return ApiResponse.OK<List<GetUserProfileDtos>>(profileDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
