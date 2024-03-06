using Project.Data.Repository.MSSQL;
using Project.ProfileService.Data;
using Project.ProfileService.Data.Configurations;

namespace Project.ProfileService.Repository.HealthProfileRepository
{
    public class HealthProfileRepository : MSSQLRepository<ApplicationDbContext, HealthProfile>, IHealthProfileRepository
    {
        public HealthProfileRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
