using Project.BookingService.Data;
using Project.Data.Repository.MSSQL;
using System.Linq.Expressions;

namespace Project.BookingService.Repository.BookingDoctorRepository;

public interface IBookingDoctorRepository : IMSSQLRepository<BookingDoctor>
{
    Task<List<BookingDoctor>> GetAllBookingDoctor(Expression<Func<BookingDoctor, bool>> filter);
    Task<BookingDoctor> GetBookingDoctor(Expression<Func<BookingDoctor, bool>> filter);
}
