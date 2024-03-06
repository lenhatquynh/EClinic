using MassTransit;
using Project.PaymentService.Events;

namespace Project.PaymentService.Consumer
{
    public class PaymentResult : IConsumer<PaymentEvent>
    {
        public Task Consume(ConsumeContext<PaymentEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
