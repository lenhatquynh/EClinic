using MassTransit;
using MediatR;
using Project.Common.Constants;
using Project.Common.Enum;
using Project.Common.Functionality;
using Project.Common.Security;
using Project.Core.Logger;
using Project.Core.RabbitMQ;
using Project.IdentityService.Commands;
using Project.IdentityService.Data;
using Project.IdentityService.Dtos;
using Project.IdentityService.Repository.UserRepository;
using Project.NotificationService.Dtos;

namespace Project.IdentityService.Handlers.Account
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<CreateUserHandler> logger;
        private readonly IBus bus;
        public CreateUserHandler(IUserRepository userRepository, ILogger<CreateUserHandler> logger, IBus bus)
        {
            this.userRepository = userRepository;
            this.logger = logger;
            this.bus = bus;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = RoleConstants.User;
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
                var userNameGeneration = role.ToLower() + request.Email.Substring(0, request.Email.IndexOf('@')) + RandomText.RandomByNumberOfCharacters(3, RandomType.Number);
                var passwordGeneration = RandomText.RandomByNumberOfCharacters(15, RandomType.String);
                var pass = Cryptography.EncryptPassword(passwordGeneration);
                var user = new User { UserName = userNameGeneration, PasswordHash = pass.Hash, PasswordSalt = pass.Salt, RoleID = role, Enabled = request.Enabled };
                var result = await userRepository.CreateEntityAsync(user);
                if (result == null)
                {
                    throw new Exception("Create User Erorr.");
                }
                var account = new ProviderAccountDtos { UserName = userNameGeneration, Password = passwordGeneration };
                if (user.Enabled == true)
                {
                    await bus.SendMessageWithExchangeName<AccountDtos>(new AccountDtos
                    {
                        Email = request.Email,
                        UserName = user.UserName,
                        Password = passwordGeneration
                    }, ExchangeConstants.NotificationService + "SendAccount");
                }
                return user.UserID;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return Guid.Empty;
            }
        }
    }
}
