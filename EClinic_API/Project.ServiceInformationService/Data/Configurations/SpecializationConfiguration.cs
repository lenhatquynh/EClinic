using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.ServiceInformationService.Data.Configurations;

public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.HasKey(x => x.SpecializationID);
        builder.Property(x => x.SpecializationID).HasDefaultValueSql("NEWID()").IsRequired();
        builder.Property(x => x.SpecializationName).IsRequired();
        builder.HasMany(x => x.Services)
               .WithOne(service => service.Specialization)
               .HasForeignKey(service => service.SpecializationID)
               .HasConstraintName("PK_Specialization_One_To_Many_Service")
               .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Specialization");

    }
}


