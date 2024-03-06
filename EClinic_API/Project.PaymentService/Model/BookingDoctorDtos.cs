namespace Project.PaymentService.Model
{
    public class BookingDoctorDtos
    {
        public Guid DoctorID { get; set; }
        public Guid UserID { get; set; }
        public Guid ProfileID { get; set; }
        public double Price { get; set; }
        public DateTime BookingTime { get; set; }
        public BookingType BookingType { get; set; }
        public Guid ScheduleID { get; set; }
    }
}
