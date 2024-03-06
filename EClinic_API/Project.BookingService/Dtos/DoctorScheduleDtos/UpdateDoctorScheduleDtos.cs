namespace Project.BookingService.Dtos.DoctorScheduleDtos
{
    public class UpdateDoctorScheduleDtos
    {
        public Guid CalenderID { get; set; }
        public List<UpdateSlotDtos> Slots { get; set; }
    }
}
