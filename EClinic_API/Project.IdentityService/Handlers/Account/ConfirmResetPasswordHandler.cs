using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Response;
using Project.Core.Caching.Service;
using Project.Core.Logger;
using Project.IdentityService.Commands;
using Project.IdentityService.Dtos;
using Project.IdentityService.Repository.UserRepository;

namespace Project.IdentityService.Handlers.Account
{
    public class ConfirmResetPasswordHandler : IRequestHandler<ConfirmResetPasswordCommand, ObjectResult>
    {
        private readonly IUserRepository userRepository;
        private readonly IResponseCacheService cacheService;
        private readonly ILogger<ConfirmResetPasswordHandler> logger;

        public ConfirmResetPasswordHandler(IUserRepository userRepository, IResponseCacheService cacheService, ILogger<ConfirmResetPasswordHandler> logger)
        {
            this.userRepository = userRepository;
            this.cacheService = cacheService;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(ConfirmResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await cacheService.GetCacheResponseAsync(request.ConfirmDataDtos.Key);
                var data = JsonConvert.DeserializeObject<DataCodeDtos>(res);
                if (data.Code != request.ConfirmDataDtos.Code)
                {
                    return ApiResponse.NotFound("Incorrect code");
                }
                var result = await userRepository.UpdateAsync(data.User);
                if (!result)
                {
                    return ApiResponse.InternalServerError();
                }
                return ApiResponse.OK("Update Success");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
