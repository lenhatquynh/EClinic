using Project.BookingService.Data;
using Project.Data.Repository.MSSQL;
using System.Linq.Expressions;

namespace Project.BookingService.Repository.DoctorCalendarRepository;

public interface IDoctorCalendarRepository : IMSSQLRepository<DoctorCalendar>
{
    Task<DoctorCalendar> GetDoctorCalendarAsync(Expression<Func<DoctorCalendar, bool>> filter);
    Task<DoctorCalendar> GetDoctorCalendarForUserAsync(Expression<Func<DoctorCalendar, bool>> filter);
}
