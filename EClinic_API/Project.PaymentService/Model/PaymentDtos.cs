namespace Project.PaymentService.Model
{
    public class PaymentDtos
    {
        public Guid PaymentID { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime PaymentTime { get; set; }
        public string PaymentService { get; set; }
        public bool IsRefund { get; set; }
    }
}
