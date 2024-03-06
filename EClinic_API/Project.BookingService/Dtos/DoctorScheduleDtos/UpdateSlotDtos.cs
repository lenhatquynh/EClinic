namespace Project.BookingService.Dtos.DoctorScheduleDtos
{
    public class UpdateSlotDtos
    {
        public Guid SlotID { get; set; } = Guid.Empty;
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
