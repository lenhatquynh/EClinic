using MassTransit;
using Project.Core.Logger;
using Project.NotificationService.Dtos;
using Project.NotificationService.Service;

namespace Project.NotificationService.Consumer
{
    public class SendMailConsumer : IConsumer<VerifyEmail>
    {
        private readonly IMailService mailService;
        private ILogger<SendMailConsumer> logger;

        public SendMailConsumer(IMailService mailService, ILogger<SendMailConsumer> logger)
        {
            this.mailService = mailService;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<VerifyEmail> context)
        {
            try
            {
                int type = context.Message.Type;
                string email = context.Message.Email;
                string code = context.Message.Code;
                Console.WriteLine("SendMailConsumer");
                Console.WriteLine("Email: " + email);
                if (type == 0) {
                   await mailService.ConfirmEmail(email, code);
                }
                if(type == 1)
                {
                    await mailService.VerifyEmail(email, code);
                }
                Console.WriteLine("Send mail");
            }catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
            }
        }
    }
}
