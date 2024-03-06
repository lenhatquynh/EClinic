using MassTransit;
using Project.Common.Constants;
using Project.Core.RabbitMQ;
using Project.IdentityService.Events;
using Project.IdentityService.Repository.UserRepository;

namespace Project.IdentityService.Consumer
{
    public class DeleteProfileConsumer : IConsumer<DeleteProfileEvents>
    {
        private readonly IUserRepository userRepository;
        private readonly IBus bus;

        public DeleteProfileConsumer(IUserRepository userRepository, IBus bus)
        {
            this.userRepository = userRepository;
            this.bus = bus;
        }

        public async Task Consume(ConsumeContext<DeleteProfileEvents> context)
        {
            var UserID = context.Message.UserID;
            var user = await userRepository.GetAsync(UserID);
            if (user == null)
            {
                await bus.SendMessageWithExchangeName<DeleteProfileResultEvents>(new DeleteProfileResultEvents { IsSuccess= false,UserID = UserID }, ExchangeConstants.ProfileService);
            }
            var result = await userRepository.DeleteAsync(user);
            if (!result)
            {
                await bus.SendMessageWithExchangeName<DeleteProfileResultEvents>(new DeleteProfileResultEvents { IsSuccess = false, UserID = UserID }, ExchangeConstants.ProfileService);
            }
            await bus.SendMessageWithExchangeName<DeleteProfileResultEvents>(new DeleteProfileResultEvents { IsSuccess = true }, ExchangeConstants.ProfileService);
        }
    }
}
