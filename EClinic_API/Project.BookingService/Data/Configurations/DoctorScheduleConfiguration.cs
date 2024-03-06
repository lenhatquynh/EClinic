using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.BookingService.Data.Configurations;

public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
{
    public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
    {
        builder.HasKey(x => x.ScheduleID);
        builder.Property(x => x.ScheduleID).HasDefaultValueSql("NEWID()").IsRequired();
        builder.Property(x => x.CalendarID).IsRequired();
        builder.Property(x => x.StartTime).IsRequired();
        builder.Property(x => x.EndTime).IsRequired();
        // Foreign Key
        builder.HasOne(x => x.BookingDoctor)
               .WithOne(bookingDoctor => bookingDoctor.DoctorSchedule)
               .HasForeignKey<BookingDoctor>(bookingDoctor => bookingDoctor.ScheduleID)
               .HasConstraintName("PK_DoctorSchedule_One_To_One_BookingDoctor")
               .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("DoctorSchedule");

    }
}
