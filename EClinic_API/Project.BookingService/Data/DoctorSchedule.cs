namespace Project.BookingService.Data;

public class DoctorSchedule
{
    public Guid ScheduleID { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    // Foreign Key
    public Guid CalendarID { get; set; }

    public DoctorCalendar DoctorCalendar { get; set; }
    public BookingDoctor BookingDoctor { get; set; }
}
