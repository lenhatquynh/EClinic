namespace Project.BookingService.Dtos.BookingPackageDTOs;

public class CreateBookingPackageDTO
{
    public Guid UserID { get; set; }
    public Guid ProfileID { get; set; }
    public double Price { get; set; }
    public Guid ServicePackageID { get; set; }
    public DateTime AppoinmentTime { get; set; }
}
