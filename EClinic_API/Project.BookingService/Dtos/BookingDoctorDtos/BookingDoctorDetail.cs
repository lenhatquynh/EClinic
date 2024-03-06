using Project.BookingService.Data;
using Project.BookingService.Dtos.Other;

namespace Project.BookingService.Dtos.BookingDoctorDtos
{
    public class BookingDoctorDetail
    {
        public Guid BookingID { get; set; }
        public ProfileDetail Profile { get; set; }
        public DateTime BookingTime { get; set; }
        public BookingType BookingType { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public Guid RoomID { get; set; }
        public DoctorSlotDtos Slot { get; set; }
    }
}
