namespace Project.PaymentService.Data
{
    public class Refund
    {
        public Guid RefundID { get; set; }
        public DateTime RefundTime { get; set; }
        public double RefundAmount { get; set; }
        public string RefundReason { get; set; }
        public Guid PaymentID { get; set; }
        public Payment Payment { get; set; }
    }
}
