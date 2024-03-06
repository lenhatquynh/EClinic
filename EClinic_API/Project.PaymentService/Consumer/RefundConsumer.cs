using MassTransit;
using MediatR;
using Project.Common.Model;
using Project.Core.Logger;
using Project.PaymentService.Commands;
using Project.PaymentService.Events;
using Project.PaymentService.Model;

namespace Project.PaymentService.Consumer
{
    public class RefundConsumer : IConsumer<RefundEvent>
    {
        private readonly IMediator mediator;
        private readonly ILogger<RefundConsumer> logger;

        public RefundConsumer(IMediator mediator, ILogger<RefundConsumer> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<RefundEvent> context)
        {
            try
            {
                await mediator.Send(new CreateRefundAutoCommand(new RefundModel { PaymentID = context.Message.BookingID,Reason = "Booking Cancel" }, "::1"));
            }catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
            }
        }
    }
}
