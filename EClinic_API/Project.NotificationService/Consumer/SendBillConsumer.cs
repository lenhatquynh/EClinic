using MassTransit;
using Project.Core.Logger;
using Project.NotificationService.Dtos;
using Project.NotificationService.Service;

namespace Project.NotificationService.Consumer
{
    public class SendBillConsumer : IConsumer<PaymentModelData>
    {
        private readonly IMailService mailService;
        private ILogger<SendBillConsumer> logger;

        public SendBillConsumer(IMailService mailService, ILogger<SendBillConsumer> logger)
        {
            this.mailService = mailService;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentModelData> context)
        {
            try
            {
                Console.WriteLine("SendBillConsumer");
                Console.WriteLine("Email: " + context.Message.Email);
                await mailService.SendBill(context.Message.Email, context.Message);
                Console.WriteLine("Send mail");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
            }
        }
    }
}
