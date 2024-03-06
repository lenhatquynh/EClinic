namespace Project.Common.Model
{
    public class RefundEvent
    {
        public Guid BookingID { get; set; }
        public double Price { get; set; }
        public int BookingType { get; set; }
    }
}
