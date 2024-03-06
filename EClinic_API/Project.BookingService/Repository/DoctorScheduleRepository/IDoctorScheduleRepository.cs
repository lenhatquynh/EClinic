using Project.BookingService.Data;
using Project.Data.Repository.MSSQL;

namespace Project.BookingService.Repository.DoctorScheduleRepository;

public interface IDoctorScheduleRepository : IMSSQLRepository<DoctorSchedule>
{

}
