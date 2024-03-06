using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ProfileService.Data;

namespace Project.IdentityService.Data.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(p => p.ProfileID);
            builder.Property(p => p.ProfileID).HasDefaultValueSql("NEWID()").IsRequired();
            builder.HasIndex(p => p.Email).IsUnique();
            builder.Property(p => p.UserID).IsRequired();
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.LastName).IsRequired();
            builder.Property(p => p.Avatar);
            builder.Property(p => p.Gender).IsRequired();
            builder.Property(p => p.DateOfBirth).IsRequired();
            builder.Property(p => p.Address);
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.Phone);
            builder.ToTable("Profiles");
        }
    }
}
