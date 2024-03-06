using MassTransit;
using Project.ProfileService.Events;

namespace Project.ProfileService.Consumer
{
    public class DeleteProfileConsumer : IConsumer<DeleteProfileEvents>
    {
        public Task Consume(ConsumeContext<DeleteProfileEvents> context)
        {
            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }
    }
}
