using Microsoft.EntityFrameworkCore;

namespace Project.BookingService.Data.Configurations;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<BookingPackage> BookingPackages { get; set; }
    public DbSet<DoctorCalendar> DoctorCalendars { get; set; }
    public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
    public DbSet<BookingDoctor> BookingDoctors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookingPackageConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorCalendarConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new BookingDoctorConfiguration());

    }
}