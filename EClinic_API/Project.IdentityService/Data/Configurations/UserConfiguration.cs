using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.IdentityService.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.UserID);
            builder.Property(user => user.UserID).HasDefaultValueSql("NEWID()").IsRequired();
            builder.Property(user => user.UserName).IsRequired();
            builder.Property(user => user.PasswordHash).IsRequired();
            builder.Property(user => user.PasswordSalt).IsRequired();
            builder.Property(user => user.Enabled).IsRequired();
            builder.Property(user => user.CreatedAt).IsRequired();
            builder.Property(user => user.UpdatedAt).IsRequired();
            builder.ToTable("Users");
        }
    }
}
