using MediatR;
using Project.Common.Constants;
using Project.IdentityService.Data;
using Project.IdentityService.Queries;
using Project.IdentityService.Repository.UserRepository;

namespace Project.IdentityService.Handlers.Account
{
    public class GetAllUserWithRoleHandler : IRequestHandler<GetAllUserWithRoleQuery, List<User>>
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<GetAllUserWithRoleHandler> logger;

        public GetAllUserWithRoleHandler(IUserRepository userRepository, ILogger<GetAllUserWithRoleHandler> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task<List<User>> Handle(GetAllUserWithRoleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var role = RoleConstants.IDUser;
                switch (request.Role)
                {
                    case RoleConstants.Admin:
                        role = RoleConstants.IDAdmin;
                        break;
                    case RoleConstants.Supporter:
                        role = RoleConstants.IDSupporter;
                        break;
                    case RoleConstants.Doctor:
                        role = RoleConstants.IDDoctor;
                        break;
                    case RoleConstants.Expert:
                        role = RoleConstants.IDExpert;
                        break;
                    case RoleConstants.User:
                        role = RoleConstants.IDUser;
                        break;
                }
                var user = await userRepository.GetAllAsync(x => string.Equals(x.RoleID, role));
               
                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}
