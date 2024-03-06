using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ProfileService.Data;
using System.Reflection.Emit;

namespace Project.IdentityService.Data.Configurations
{
    public class HealthProfileConfiguration : IEntityTypeConfiguration<HealthProfile>
    {
        public void Configure(EntityTypeBuilder<HealthProfile> builder)
        {
            builder.HasKey(p => p.ProfileID);
            builder.Property(p => p.ProfileID).IsRequired();
            builder.Property(p => p.BloodType);
            builder.Property(p => p.Height);
            builder.Property(p => p.Weight);
            builder.Property(p => p.RelationshipID).IsRequired();
            builder.HasOne(p => p.Profile)
                   .WithOne(pp => pp.HealthProfile)
                   .HasForeignKey<HealthProfile>(p => p.ProfileID)
                   .HasConstraintName("PK_Profile_One_To_One_HealthProfile")
                   .OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("PatientProfiles");
        }
    }
}
