using Google.Protobuf.WellKnownTypes;
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
using Project.IdentityService.Data;
using Project.IdentityService.Dtos;
using Project.IdentityService.Protos;
using Project.IdentityService.Repository.UserRepository;
using Project.NotificationService.Dtos;

namespace Project.IdentityService.Handlers.Account
{
    public class SignUpHandler : IRequestHandler<SignUpCommand, ObjectResult>
    {
        private readonly IUserRepository userRepository;
        private readonly ProfileService.ProfileServiceClient client;
        private readonly ILogger<SignUpHandler> logger;
        private readonly IResponseCacheService cacheService;
        private readonly IBus bus;

        public SignUpHandler(IUserRepository userRepository, IConfiguration configuration, ILogger<SignUpHandler> logger, IResponseCacheService cacheService, IBus bus)
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

        public async Task<ObjectResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var isUserNameExist = await userRepository.AnyAsync(user => user.UserName == request.SignUpDtos.UserName);
                if (isUserNameExist)
                {
                    return ApiResponse.BadRequest("User Name is exist");
                }
                if (!request.SignUpDtos.Password.Equals(request.SignUpDtos.ConfirmPassword))
                {
                    return ApiResponse.BadRequest("Password and confirm password are not the same");
                }
                var checkEmail = await client.EmailIsExistAsync(new CheckEmailRequest { Email = request.SignUpDtos.Email });
                if (checkEmail.IsExist)
                {
                    return ApiResponse.BadRequest("Email is exist");
                }
                var pass = Cryptography.EncryptPassword(request.SignUpDtos.Password);
                var user = new User { UserName = request.SignUpDtos.UserName, PasswordHash = pass.Hash, PasswordSalt = pass.Salt, RoleID = RoleConstants.IDUser, CreatedAt = DateTime.Now };
                var code = RandomText.RandomByNumberOfCharacters(6, RandomType.Number);
                CreateProfileRequest profile = new CreateProfileRequest
                {
                    Email = request.SignUpDtos.Email.ToLower(),
                    FirstName = request.SignUpDtos.FirstName,
                    LastName = request.SignUpDtos.LastName,
                    Gender = request.SignUpDtos.Gender,
                    DateOfBirth = request.SignUpDtos.DateOfBirth.ToTimestamp()
                };
                var TextBytes = System.Text.Encoding.UTF8.GetBytes(request.SignUpDtos.Email + user.UserName);
                var key = Convert.ToBase64String(TextBytes);
                var DataCode = new DataCodeDtos { User = user, Code = code,CreateProfileRequest = profile };
                await cacheService.SetCacheResponseAsync(key, DataCode, TimeSpan.FromHours(1));
                var data = new ConfirmDataDtos { Key = key, Code = code };
                var content = new VerifyEmail { Email = request.SignUpDtos.Email, Code = code, Type = 0 };
                await bus.SendMessageWithExchangeName<VerifyEmail>(content, ExchangeConstants.NotificationService);
                await bus.SendMessageWithExchangeName<VerifyEmail>(content, ExchangeConstants.NotificationService);
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
