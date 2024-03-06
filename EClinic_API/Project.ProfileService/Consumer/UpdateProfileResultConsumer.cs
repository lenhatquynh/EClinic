using MassTransit;
using Project.Core.RabbitMQ;
using Project.ProfileService.Events;
using Project.ProfileServices.Events;

namespace Project.ProfileService.Consumer
{
    public class UpdateProfileResultConsumer : IConsumer<UpdateProfileResultEvents>
    {
        private readonly IBus bus;

        public UpdateProfileResultConsumer(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Consume(ConsumeContext<UpdateProfileResultEvents> context)
        {
            var result = context.Message.IsSuccess;
            if (!result)
            {
                await bus.SendMessage(new UpdateProfileEvents
                {
                    UserID = context.Message.UserID,
                    Avatar = context.Message.Avatar,
                    FirstName = context.Message.FirstName,
                    LastName = context.Message.LastName
                });
            }
        }
    }
}
