using Microsoft.EntityFrameworkCore;

namespace Project.ServiceInformationService.Data.Configurations;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServicePackage> ServicePackages { get; set; }
    public DbSet<ServicePackageItem> ServicePackageItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SpecializationConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceConfiguration());
        modelBuilder.ApplyConfiguration(new ServicePackageConfiguration());
        modelBuilder.ApplyConfiguration(new ServicePackageItemConfiguration());

    }
}