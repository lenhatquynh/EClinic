using Grpc.Net.Client;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Enum;
using Project.Common.Functionality;
using Project.Common.Response;
using Project.Common.Security;
using Project.Core.Caching.Service;
using Project.Core.Logger;
using Project.Core.RabbitMQ;
using Project.IdentityService.Commands;
using Project.IdentityService.Dtos;
using Project.IdentityService.Protos;
using Project.IdentityService.Repository.UserRepository;
using Project.NotificationService.Dtos;

namespace Project.IdentityService.Handlers.Account
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, ObjectResult>
    {
        private readonly IUserRepository userRepository;
        private readonly ProfileService.ProfileServiceClient client;
        private readonly ILogger<ResetPasswordHandler> logger;
        private readonly IResponseCacheService cacheService;
        private readonly IBus bus;
        public ResetPasswordHandler(IUserRepository userRepository, IConfiguration configuration, ILogger<ResetPasswordHandler> logger, IResponseCacheService cacheService, IBus bus)
        {
            this.userRepository = userRepository;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
            this.logger = logger;
            this.cacheService = cacheService;
            this.bus = bus;
        }

        public async Task<ObjectResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var resultCheckMail = await client.EmailIsExistAsync(new CheckEmailRequest { Email = request.ResetPasswordDTO.Email });
                if (!resultCheckMail.IsExist)
                {
                    return ApiResponse.NotFound("Account not found");
                }
                if (!string.Equals(request.ResetPasswordDTO.NewPassword, request.ResetPasswordDTO.ConfirmPassword))
                {
                    return ApiResponse.BadRequest("Password and Confirm Password are not the same");
                }
                var userID = Guid.Parse(resultCheckMail.UserID);
                var user = await userRepository.GetAsync(userID);
                var pass = Cryptography.EncryptPassword(request.ResetPasswordDTO.NewPassword);
                user.PasswordSalt = pass.Salt;
                user.PasswordHash = pass.Hash;
                var TextBytes = System.Text.Encoding.UTF8.GetBytes(request.ResetPasswordDTO.Email + user.UserName + "Reset");
                var key = Convert.ToBase64String(TextBytes);
                var code = RandomText.RandomByNumberOfCharacters(6, RandomType.Number);
                var DataCode = new DataCodeDtos { User = user, Code = code, CreateProfileRequest = new CreateProfileRequest { Email = request.ResetPasswordDTO.Email } };
                await cacheService.SetCacheResponseAsync(key, DataCode, TimeSpan.FromHours(1));
                var data = new ConfirmDataDtos { Key = key, Code = code };
                await bus.SendMessageWithExchangeName<VerifyEmail>(new VerifyEmail { Email = request.ResetPasswordDTO.Email, Code = code, Type = 1 }, ExchangeConstants.NotificationService);
                await bus.SendMessageWithExchangeName<VerifyEmail>(new VerifyEmail { Email = request.ResetPasswordDTO.Email, Code = code, Type = 1 }, ExchangeConstants.NotificationService);
                return ApiResponse.OK<string>(key);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
