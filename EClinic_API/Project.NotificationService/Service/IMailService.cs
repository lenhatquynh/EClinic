using Project.NotificationService.Dtos;

namespace Project.NotificationService.Service
{
    public interface IMailService
    {
        Task<bool> VerifyEmail(string email, string code);
        Task<bool> ConfirmEmail(string email, string code);
        Task<bool> SendBill(string email, PaymentModelData paymentModel);
        Task<bool> SendAccount(string email, AccountDtos account);
    }
}
