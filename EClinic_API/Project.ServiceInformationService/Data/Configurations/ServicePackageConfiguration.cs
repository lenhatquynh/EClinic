using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.ServiceInformationService.Data.Configurations;

public class ServicePackageConfiguration : IEntityTypeConfiguration<ServicePackage>
{
    public void Configure(EntityTypeBuilder<ServicePackage> builder)
    {
        builder.HasKey(x => x.ServicePackageID);
        builder.Property(x => x.ServicePackageID).HasDefaultValueSql("NEWID()").IsRequired();
        builder.Property(x => x.ServicePackageName).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Image).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Discount).IsRequired();
        builder.Property(x => x.TotalOrder).IsRequired();
        builder.Property(x => x.EstimatedTime).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
        // Relationship
        builder.HasMany(x => x.ServicePackageItems)
               .WithOne(servicePackageItem => servicePackageItem.ServicePackage)
               .HasForeignKey(servicePackageItem => servicePackageItem.ServicePackageID)
               .HasConstraintName("PK_ServicePackage_One_To_Many_ServicePackageItem")
               .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("ServicePackage");

    }
}