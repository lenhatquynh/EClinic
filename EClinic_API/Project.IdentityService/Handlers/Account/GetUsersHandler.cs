using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Response;
using Project.Core.Logger;
using Project.IdentityService.Dtos;
using Project.IdentityService.Queries;
using Project.IdentityService.Repository.UserRepository;

namespace Project.IdentityService.Handlers.Account
{
    public class GetUsersHandler : IRequestHandler<GetAllUserQuery, ObjectResult>
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<GetUsersHandler> logger;
        private readonly IMapper mapper;

        public GetUsersHandler(IUserRepository userRepository, ILogger<GetUsersHandler> logger, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<ObjectResult> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var paginationModel = await userRepository.GetUsersAsync(request.PaginationRequestHeader, request.SearchUserDtos);
                var usersDtos = mapper.Map<List<GetUsersDtos>>(paginationModel.PaginationData);
                request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationModel.PaginationResponseHeader));
                return ApiResponse.OK<object>(usersDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
