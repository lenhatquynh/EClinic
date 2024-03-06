using MassTransit;
using MediatR;
using Project.Common.Constants;
using Project.Common.Enum;
using Project.Common.Functionality;
using Project.Common.Security;
using Project.Core.Logger;
using Project.Core.RabbitMQ;
using Project.IdentityService.Commands;
using Project.IdentityService.Repository.UserRepository;
using Project.NotificationService.Dtos;

namespace Project.IdentityService.Handlers.Account
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<UpdateUserHandler> logger;
        private readonly IBus bus;
        public UpdateUserHandler(IUserRepository userRepository, ILogger<UpdateUserHandler> logger, IBus bus)
        {
            this.userRepository = userRepository;
            this.logger = logger;
            this.bus = bus;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userRepository.GetAsync(request.UserID);
                if (user == null)
                {
                    return false;
                }
                if (user.Enabled == request.Enabled)
                {
                    return true;
                }
                user.Enabled = request.Enabled;
                if (user.Enabled == true)
                {
                    var passwordGeneration = RandomText.RandomByNumberOfCharacters(15, RandomType.String);
                    var pass = Cryptography.EncryptPassword(passwordGeneration);
                    user.PasswordSalt = pass.Salt;
                    user.PasswordHash = pass.Hash;
                    await bus.SendMessageWithExchangeName<AccountDtos>(new AccountDtos
                    {
                        Email = request.Email,
                        UserName = user.UserName,
                        Password = passwordGeneration
                    }, ExchangeConstants.NotificationService + "SendAccount");
                }
                var result = await userRepository.UpdateAsync(user);
                if (!result)
                {
                    throw new Exception("Update User Erorr.");
                }
                
                
                return result;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return false;
            }
        }
    }
}
