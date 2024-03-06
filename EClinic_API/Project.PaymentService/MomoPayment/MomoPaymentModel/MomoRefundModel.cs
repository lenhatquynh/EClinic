namespace Project.PaymentService.MomoPayment.MomoPaymentModel
{
    public class MomoRefundModel
    {
        public string TransactionID { get; set; }
        public double Amount { get; set; }
        public string Message { get; set; }
        public Guid UserID { get; set; }
        public Guid BookingID { get; set; }
        public string OrderID { get; set; }
    }
}
