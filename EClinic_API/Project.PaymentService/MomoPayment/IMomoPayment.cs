using Project.PaymentService.Model;
using Project.PaymentService.MomoPayment.MomoPaymentModel;

namespace Project.PaymentService.MomoPayment
{
    public interface IMomoPayment
    {
        Task<string> PaymentRequest(PaymentModel PaymentModel);
        Task<RefundResult> PaymentRefund(MomoRefundModel RefundModel);
        PaymentResult PaymentConfirm(IQueryCollection query);
    }
}
