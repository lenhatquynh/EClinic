namespace Project.PaymentService.Model
{
    public class TransactionQueryModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeType TimeType { get; set; }
    }
}
