namespace Project.BookingService.Data;

public class BookingDoctor
{
    public Guid BookingID { get; set; }
    public Guid DoctorID { get; set; }
    public Guid UserID { get; set; }
    public Guid ProfileID { get; set; }
    public double Price { get; set; }
    public DateTime BookingTime { get; set; }
    public BookingType BookingType { get; set; }
    public BookingStatus BookingStatus { get; set; }
    public Guid RoomID { get; set; }

    // Relationship
    public Guid ScheduleID { get; set; }
    public DoctorSchedule DoctorSchedule { get; set; }

}
