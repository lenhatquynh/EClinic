namespace Project.BookingService.Dtos.DoctorScheduleDtos
{
    public class SlotDtos
    {
        public Guid SlotID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsBooking { get; set; } = false;
    }
}
