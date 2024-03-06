using Microsoft.EntityFrameworkCore;
using Project.BookingService.Data;
using Project.BookingService.Data.Configurations;
using Project.Data.Repository.MSSQL;
using System.Linq.Expressions;

namespace Project.BookingService.Repository.BookingDoctorRepository;

public class BookingDoctorRepository : MSSQLRepository<ApplicationDbContext, BookingDoctor>, IBookingDoctorRepository
{
    private readonly ApplicationDbContext context;
    public BookingDoctorRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        this.context = dbContext;
    }

    public async Task<List<BookingDoctor>> GetAllBookingDoctor(Expression<Func<BookingDoctor, bool>> filter)
    {
        return await context.BookingDoctors.Include(x => x.DoctorSchedule).ThenInclude(x => x.DoctorCalendar).Where(filter).ToListAsync();
    }

    public async Task<BookingDoctor> GetBookingDoctor(Expression<Func<BookingDoctor, bool>> filter)
    {
        return await context.BookingDoctors.Include(x => x.DoctorSchedule).SingleOrDefaultAsync(filter);
    }
}
