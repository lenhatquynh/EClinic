namespace Project.PaymentService.Model
{
    public class RefundDetailDtos
    {
        public Guid RefundID { get; set; }
        public DateTime RefundTime { get; set; }
        public double RefundAmount { get; set; }
        public string RefundReason { get; set; }
        public string PaymentService { get; set; }
    }
}
