namespace Project.BookingService.Data;

public class BookingPackage
{
    public Guid BookingID { get; set; }
    public Guid UserID { get; set; }
    public Guid ProfileID { get; set; }
    public double Price { get; set; }
    public DateTime BookingTime { get; set; }
    public Guid ServicePackageID { get; set; }
    public DateTime AppoinmentTime { get; set; }
    public BookingStatus BookingStatus { get; set; }
}
