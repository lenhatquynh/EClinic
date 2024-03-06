using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.BookingService.Data.Configurations;

public class DoctorCalendarConfiguration : IEntityTypeConfiguration<DoctorCalendar>
{
    public void Configure(EntityTypeBuilder<DoctorCalendar> builder)
    {
        builder.HasKey(x => x.CalenderID);
        builder.Property(x => x.CalenderID).HasDefaultValueSql("NEWID()").IsRequired();
        builder.Property(x => x.DoctorID).IsRequired();
        builder.Property(x => x.Time).IsRequired();
        // Foreign Key
        builder.HasMany(x => x.DoctorSchedules)
              .WithOne(doctorSchedule => doctorSchedule.DoctorCalendar)
              .HasForeignKey(doctorSchedule => doctorSchedule.CalendarID)
              .HasConstraintName("PK_DoctorCalendar_One_To_Many_DoctorSchedule")
              .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("DoctorCalendar");

    }
}
