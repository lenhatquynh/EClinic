using Project.BookingService.Data;
using Project.BookingService.Dtos.Other;

namespace Project.BookingService.Dtos.BookingPackageDTOs
{
    public class BookingPackageDetail
    {
        public Guid BookingID { get; set; }
        public ProfileDetail Profile { get; set; }
        public double Price { get; set; }
        public DateTime BookingTime { get; set; }
        public ServiceInformation Service { get; set; }
        public DateTime AppoinmentTime { get; set; }
        public BookingStatus BookingStatus { get; set; }
    }
}
