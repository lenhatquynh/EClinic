namespace Project.PaymentService.Model
{
    public class PaymentResult
    {
        public bool IsSuccess { get; set; }
        public double Amount { get; set; }
        public string OrderID { get; set; }
        public DateTime PaymentTime { get; set; }
        public Guid UserID { get; set; }
        public Guid BookingID { get; set; }
        public string TransactionID { get; set; }
        public string PaymentType { get; set; }
    }
}
