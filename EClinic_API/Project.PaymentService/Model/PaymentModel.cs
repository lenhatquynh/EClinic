namespace Project.PaymentService.Model
{
    public class PaymentModel
    {
        public Guid BookingID { get; set; }
        public Guid UserID { get; set; }
        public double Amount { get; set; }
        public string Message { get; set; }
    }
}
