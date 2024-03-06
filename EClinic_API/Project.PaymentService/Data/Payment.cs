namespace Project.PaymentService.Data
{
    public class Payment
    {
        public Guid PaymentID { get; set; }
        public string TransactionID { get; set; }
        public string OrderID { get; set; }
        public double PaymentAmount { get; set; }
        public Guid UserID { get; set; }
        public Guid BookingID { get; set; }
        public DateTime PaymentTime { get; set; }
        public PaymentService PaymentService { get; set; }
        public Refund Refund { get; set; }
    }
}
