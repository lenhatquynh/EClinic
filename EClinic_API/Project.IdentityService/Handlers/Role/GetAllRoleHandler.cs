using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.IdentityService.Dtos;
using Project.IdentityService.Queries;
using Project.IdentityService.Repository.RoleRepository;

namespace Project.IdentityService.Handlers.Roles
{
    public class GetAllRoleHandler : IRequestHandler<GetAllRoleQuery, ObjectResult>
    {
        private readonly ILogger<GetAllRoleHandler> logger;
        private readonly IRoleRepository roleRepository;
        private readonly IMapper mapper;

        public GetAllRoleHandler(ILogger<GetAllRoleHandler> logger, IRoleRepository roleRepository, IMapper mapper)
        {
            this.logger = logger;
            this.roleRepository = roleRepository;
            this.mapper = mapper;
        }

        public async Task<ObjectResult> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var roles = await roleRepository.GetAllAsync();
                var roleDtos = mapper.Map<List<RoleDtos>>(roles);
                return ApiResponse.OK<List<RoleDtos>>(roleDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
