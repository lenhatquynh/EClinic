using Project.BookingService.Data;
using Project.BookingService.Dtos.Other;

namespace Project.BookingService.Dtos.BookingDoctorDtos
{
    public class BookingDoctorDTO
    {
        public Guid BookingID { get; set; }
        public ProfileDtos DoctorProfile { get; set; }
        public ProfileDtos UserProfile { get; set; }
        public double Price { get; set; }
        public DateTime BookingTime { get; set; }
        public BookingType BookingType { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public DoctorSlotDtos Slot { get; set; }
        public Guid RoomID { get; set; }
        public DateTime BookingCalendar { get; set; }
    }
}
