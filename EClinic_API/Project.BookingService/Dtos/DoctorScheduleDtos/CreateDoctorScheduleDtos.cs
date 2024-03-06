namespace Project.BookingService.Dtos.DoctorScheduleDtos
{
    public class CreateDoctorScheduleDtos
    {
        public Guid DoctorID { get; set; }
        public DateTime Time { get; set; }
        public List<CreateSlotDtos> Slots { get; set; }
    }
}
