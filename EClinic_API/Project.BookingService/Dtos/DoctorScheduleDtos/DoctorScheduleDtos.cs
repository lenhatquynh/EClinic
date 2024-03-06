namespace Project.BookingService.Dtos.DoctorScheduleDtos
{
    public class DoctorScheduleDtos
    {
        public Guid CalenderID { get; set; }
        public DateTime Time { get; set; }
        public List<SlotDtos> Slots { get; set; }
    }
}
