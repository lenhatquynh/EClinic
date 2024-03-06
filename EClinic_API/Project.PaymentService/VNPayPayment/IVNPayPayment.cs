using Project.PaymentService.Model;
using Project.PaymentService.MomoPayment.MomoPaymentModel;
using Project.PaymentService.VNPayPayment.VNPayPaymentModel;

namespace Project.PaymentService.VNPayPayment
{
    public interface IVNPayPayment
    {
        string PaymentRequest(PaymentModel PaymentModel, string ipAddress);
        Task<RefundResult> PaymentRefund(VNPayRefundModel RefundModel, string ipAddress);
        PaymentResult PaymentConfirm(IQueryCollection query);
        
    }
}
