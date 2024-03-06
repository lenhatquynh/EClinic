using MassTransit;
using Project.Core.Logger;
using Project.NotificationService.Dtos;
using Project.NotificationService.Service;

namespace Project.NotificationService.Consumer
{
    public class SendAccountConsumer : IConsumer<AccountDtos>
    {
        private readonly IMailService mailService;
        private ILogger<SendAccountConsumer> logger;

        public SendAccountConsumer(IMailService mailService, ILogger<SendAccountConsumer> logger)
        {
            this.mailService = mailService;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<AccountDtos> context)
        {
            try
            {
                Console.WriteLine("SendAccountConsumer");
                Console.WriteLine("Email: "+ context.Message.Email);
                await mailService.SendAccount(context.Message.Email, context.Message);
                Console.WriteLine("Send mail");
            }catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
            }
        }
    }
}
