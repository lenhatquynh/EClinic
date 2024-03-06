using Project.BookingService.Data;

namespace Project.BookingService.Dtos.BookingDoctorDtos
{
    public class CreateBookingDoctorDto
    {
        public Guid DoctorID { get; set; }
        public Guid UserID { get; set; }
        public Guid ProfileID { get; set; }
        public double Price { get; set; }
        public BookingType BookingType { get; set; }
        public Guid ScheduleID { get; set; }
    }
}
