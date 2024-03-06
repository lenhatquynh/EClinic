using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.BookingService.Data.Configurations;

public class BookingPackageConfiguration : IEntityTypeConfiguration<BookingPackage>
{
    public void Configure(EntityTypeBuilder<BookingPackage> builder)
    {
        builder.HasKey(x => x.BookingID);
        builder.Property(x => x.BookingID).HasDefaultValueSql("NEWID()").IsRequired();
        builder.Property(x => x.UserID).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.BookingTime).IsRequired();
        builder.Property(x => x.ServicePackageID).IsRequired();
        builder.Property(x => x.AppoinmentTime).IsRequired();
        builder.Property(x => x.BookingStatus).IsRequired();

        builder.ToTable("BookingPackage");

    }
}
