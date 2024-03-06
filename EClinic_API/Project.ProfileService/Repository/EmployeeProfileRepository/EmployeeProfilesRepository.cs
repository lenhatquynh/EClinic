using Project.Data.Repository.MSSQL;
using Project.ProfileService.Data;
using Project.ProfileService.Data.Configurations;

namespace Project.ProfileService.Repository.EmployeeProfileRepository
{
    public class EmployeeProfilesRepository : MSSQLRepository<ApplicationDbContext, EmployeeProfile>, IEmployeeProfilesRepository
    {
        public EmployeeProfilesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
