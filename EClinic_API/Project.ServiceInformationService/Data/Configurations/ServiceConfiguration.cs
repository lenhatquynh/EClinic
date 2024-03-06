using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.ServiceInformationService.Data.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(x => x.ServiceID);
        builder.Property(x => x.ServiceID).HasDefaultValueSql("NEWID()").IsRequired();
        builder.Property(x => x.ServiceName).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.EstimatedTime).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
        builder.Property(x => x.SpecializationID).IsRequired();
        // Relationship
        builder.HasMany(x => x.ServicePackageItems)
               .WithOne(servicePackageItem => servicePackageItem.Service)
               .HasForeignKey(servicePackageItem => servicePackageItem.ServiceID)
               .HasConstraintName("PK_Service_One_To_Many_ServicePackageItem")
               .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Service");

    }
}
