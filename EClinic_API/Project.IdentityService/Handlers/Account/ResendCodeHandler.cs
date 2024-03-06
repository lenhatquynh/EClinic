using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Constants;
using Project.Common.Enum;
using Project.Common.Functionality;
using Project.Common.Response;
using Project.Core.Caching.Service;
using Project.Core.Logger;
using Project.Core.RabbitMQ;
using Project.IdentityService.Commands;
using Project.IdentityService.Dtos;
using Project.NotificationService.Dtos;

namespace Project.IdentityService.Handlers.Account
{
    public class ResendCodeHandler : IRequestHandler<ReSendCodeCommand, ObjectResult>
    {
        private readonly IBus bus;
        private ILogger<ResendCodeHandler> logger;
        private readonly IResponseCacheService cacheService;

        public ResendCodeHandler(IBus bus, ILogger<ResendCodeHandler> logger, IResponseCacheService cacheService)
        {
            this.bus = bus;
            this.logger = logger;
            this.cacheService = cacheService;
        }

        public async Task<ObjectResult> Handle(ReSendCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var code = RandomText.RandomByNumberOfCharacters(6, RandomType.Number);
                var res = await cacheService.GetCacheResponseAsync(request.ResendCodeDtos.Key);
                var data = JsonConvert.DeserializeObject<DataCodeDtos>(res);
                await cacheService.RemoveCacheAsync(request.ResendCodeDtos.Key);
                var DataCode = new DataCodeDtos { User = data.User, Code = code, CreateProfileRequest = data.CreateProfileRequest };
                await cacheService.SetCacheResponseAsync(request.ResendCodeDtos.Key, DataCode, TimeSpan.FromHours(1));
                var newdata = new ConfirmDataDtos { Key = request.ResendCodeDtos.Key, Code = code };
                await bus.SendMessageWithExchangeName<VerifyEmail>(new VerifyEmail { Email = data.CreateProfileRequest.Email, Code = code, Type = request.ResendCodeDtos.Type }, ExchangeConstants.NotificationService);
                return ApiResponse.OK("Resend success");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
