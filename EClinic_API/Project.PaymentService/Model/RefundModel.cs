namespace Project.PaymentService.Model
{
    public class RefundModel
    {
        public Guid PaymentID { get; set; }
        public string Reason { get; set; }
    }
}
