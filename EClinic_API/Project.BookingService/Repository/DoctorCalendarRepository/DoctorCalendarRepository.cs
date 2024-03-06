using Microsoft.EntityFrameworkCore;
using Project.BookingService.Data;
using Project.BookingService.Data.Configurations;
using Project.Data.Repository.MSSQL;
using System.Linq.Expressions;

namespace Project.BookingService.Repository.DoctorCalendarRepository;

public class DoctorCalendarRepository : MSSQLRepository<ApplicationDbContext, DoctorCalendar>, IDoctorCalendarRepository
{
    private readonly ApplicationDbContext context;
    public DoctorCalendarRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        this.context = dbContext;
    }

    public async Task<DoctorCalendar> GetDoctorCalendarAsync(Expression<Func<DoctorCalendar, bool>> filter)
    {
        return await context.DoctorCalendars.Include(x=>x.DoctorSchedules).SingleOrDefaultAsync(filter);
    }

    public async Task<DoctorCalendar> GetDoctorCalendarForUserAsync(Expression<Func<DoctorCalendar, bool>> filter)
    {
        return await context.DoctorCalendars.Include(x => x.DoctorSchedules).ThenInclude(x => x.BookingDoctor).SingleOrDefaultAsync(filter);
    }
}
