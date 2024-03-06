using Microsoft.EntityFrameworkCore;
using Project.IdentityService.Data.Configurations;

namespace Project.ProfileService.Data.Configurations
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<DoctorProfile> DoctorProfiles { get; set; }
        public DbSet<HealthProfile> HealthProfiles { get; set; }
        public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }
        public DbSet<Relationship> Relationships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorProfileConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeProfileConfiguration());
            modelBuilder.ApplyConfiguration(new HealthProfileConfiguration());
            modelBuilder.ApplyConfiguration(new RelationshipConfiguration());
        }
    }
}
