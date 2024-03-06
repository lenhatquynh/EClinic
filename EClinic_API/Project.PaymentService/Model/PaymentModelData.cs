namespace Project.NotificationService.Dtos
{
    public class PaymentModelData
    {
        public string Email { get; set; }
        public Guid PaymentID { get; set; }
        public string FullName { get; set; }
        public double PaymentAmount { get; set; }
        public string BookingType { get; set; }
        public DateTime PaymentTime { get; set; }
        public string PaymentService { get; set; }
        public string TransactionID { get; set; }
    }
}
