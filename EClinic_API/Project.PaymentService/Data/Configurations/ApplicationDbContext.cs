using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Common.Constants;

namespace Project.PaymentService.Data.Configurations
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Refund> Refunds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RefundConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            
        }
    }
}
