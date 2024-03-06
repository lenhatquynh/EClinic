using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.BookingService.Data.Configurations;

public class BookingDoctorConfiguration : IEntityTypeConfiguration<BookingDoctor>
{
    public void Configure(EntityTypeBuilder<BookingDoctor> builder)
    {
        builder.HasKey(x => x.BookingID);
        builder.Property(x => x.BookingID).HasDefaultValueSql("NEWID()").IsRequired();
        builder.Property(x => x.DoctorID).IsRequired();
        builder.Property(x => x.UserID).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.BookingTime).IsRequired();
        builder.Property(x => x.ScheduleID).IsRequired();
        builder.Property(x => x.BookingType).IsRequired();
        builder.Property(x => x.RoomID).IsRequired();
        builder.Property(x => x.BookingStatus).IsRequired();
        builder.ToTable("BookingDoctor");

    }
}
