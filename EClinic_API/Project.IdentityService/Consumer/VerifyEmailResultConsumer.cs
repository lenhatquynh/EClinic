using MassTransit;
using Project.IdentityService.Events;

namespace Project.IdentityService.Consumer
{
    public class VerifyEmailResultConsumer : IConsumer<VerifyEmailResultEvent>
    {
        public Task Consume(ConsumeContext<VerifyEmailResultEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
