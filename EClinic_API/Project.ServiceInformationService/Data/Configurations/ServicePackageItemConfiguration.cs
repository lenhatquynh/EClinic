using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.ServiceInformationService.Data.Configurations;

public class ServicePackageItemConfiguration : IEntityTypeConfiguration<ServicePackageItem>
{
    public void Configure(EntityTypeBuilder<ServicePackageItem> builder)
    {
        builder.HasKey(x => new { x.ServicePackageID, x.ServiceID });

        builder.ToTable("ServicePackageItem");
    }
}