using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ProfileService.Data;
using System.Reflection.Emit;

namespace Project.IdentityService.Data.Configurations
{
    public class DoctorProfileConfiguration : IEntityTypeConfiguration<DoctorProfile>
    {
        public void Configure(EntityTypeBuilder<DoctorProfile> builder)
        {
            builder.HasKey(p => p.ProfileID);
            builder.Property(p => p.ProfileID).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.Content).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.WorkStart).IsRequired();
            builder.Property(p => p.WorkEnd);
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.SpecializationID).IsRequired();
            builder.HasOne(p => p.Profile)
                  .WithOne(dp => dp.DoctorProfile)
                  .HasForeignKey<DoctorProfile>(p => p.ProfileID)
                  .HasConstraintName("PK_Profile_One_To_One_DoctorProfile")
                  .OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("DoctorProfiles");
        }
    }
}
