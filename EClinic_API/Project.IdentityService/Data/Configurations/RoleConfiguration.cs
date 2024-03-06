using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Common.Constants;
using System.Reflection.Emit;

namespace Project.IdentityService.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(role => role.RoleID);
            builder.Property(role => role.RoleID).IsRequired();
            builder.Property(role => role.RoleName).IsRequired();
            builder.HasMany(auth => auth.User)
                   .WithOne(user => user.Role)
                   .HasForeignKey(user => user.RoleID)
                   .HasConstraintName("PK_User_Many_To_One_Role")
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(
                new Role { RoleID = RoleConstants.IDAdmin, RoleName = RoleConstants.Admin },
                new Role { RoleID = RoleConstants.IDSupporter, RoleName = RoleConstants.Supporter },
                new Role { RoleID = RoleConstants.IDDoctor, RoleName = RoleConstants.Doctor },
                new Role { RoleID = RoleConstants.IDUser, RoleName = RoleConstants.User },
                new Role { RoleID = RoleConstants.IDExpert, RoleName = RoleConstants.Expert }
            );
            builder.ToTable("Roles");

        }
    }
}
