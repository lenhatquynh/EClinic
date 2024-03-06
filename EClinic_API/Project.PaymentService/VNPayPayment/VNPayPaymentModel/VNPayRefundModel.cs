namespace Project.PaymentService.MomoPayment.MomoPaymentModel
{
    public class VNPayRefundModel
    {
        public string TransactionID { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Message { get; set; }
        public Guid UserID { get; set; }
        public string OrderID { get; set; }
    }
}
