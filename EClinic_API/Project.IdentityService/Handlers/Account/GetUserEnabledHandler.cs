using MediatR;
using Project.Core.Logger;
using Project.IdentityService.Queries;
using Project.IdentityService.Repository.UserRepository;

namespace Project.IdentityService.Handlers.Account
{
    public class GetUserEnabledHandler : IRequestHandler<GetUserEnabledQuery, int>
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<GetUserEnabledHandler> logger;
        public GetUserEnabledHandler(IUserRepository userRepository, ILogger<GetUserEnabledHandler> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task<int> Handle(GetUserEnabledQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userRepository.GetAsync(request.userID);
                return Convert.ToInt32(user.Enabled);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return 2;
            }
        }
    }
}
