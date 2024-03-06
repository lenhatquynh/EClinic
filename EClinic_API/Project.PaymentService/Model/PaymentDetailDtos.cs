namespace Project.PaymentService.Model
{
    public class PaymentDetailDtos
    {
        public Guid PaymentID { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime PaymentTime { get; set; }
        public string PaymentService { get; set; }
        public Author Author { get; set; }
        public bool IsRefund { get; set; }
    }
}
