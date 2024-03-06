using Project.BookingService.Data;
using Project.BookingService.Data.Configurations;
using Project.Data.Repository.MSSQL;

namespace Project.BookingService.Repository.DoctorScheduleRepository;

public class DoctorScheduleRepository : MSSQLRepository<ApplicationDbContext, DoctorSchedule>, IDoctorScheduleRepository
{
    public DoctorScheduleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}
