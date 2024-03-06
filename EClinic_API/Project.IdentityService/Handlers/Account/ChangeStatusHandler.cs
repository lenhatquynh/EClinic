using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Caching.Service;
using Project.Core.Logger;
using Project.IdentityService.Commands;
using Project.IdentityService.Repository.UserRepository;

namespace Project.IdentityService.Handlers.Account
{
    public class ChangeStatusHandler : IRequestHandler<ChangeStatusCommand, ObjectResult>
    {
        private readonly IUserRepository userRepository;
        private readonly IResponseCacheService cacheService;
        private readonly ILogger<ChangeStatusHandler> logger;

        public ChangeStatusHandler(IUserRepository userRepository, IResponseCacheService cacheService, ILogger<ChangeStatusHandler> logger)
        {
            this.userRepository = userRepository;
            this.cacheService = cacheService;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userRepository.GetAsync(request.UserID);
                if (user == null)
                {
                    return ApiResponse.NotFound("User not found.");
                }
                user.Enabled = !user.Enabled;
                var result = await userRepository.UpdateAsync(user);
                if (!result) { throw new Exception("Update faild"); }
                if (user.Enabled)
                {
                    await cacheService.RemoveCacheAsync(request.UserID.ToString());
                }
                else
                {
                    await cacheService.SetCacheResponseAsync(request.UserID.ToString(), user.Enabled, TimeSpan.FromDays(1));
                }
                return ApiResponse.OK("Change status success");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
