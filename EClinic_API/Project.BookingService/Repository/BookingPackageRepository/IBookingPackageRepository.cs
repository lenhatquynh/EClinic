using Project.BookingService.Data;
using Project.Data.Repository.MSSQL;

namespace Project.BookingService.Repository.BookingPackageRepository;

public interface IBookingPackageRepository : IMSSQLRepository<BookingPackage>
{

}
