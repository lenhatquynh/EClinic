using Project.Data.Repository.MSSQL;
using Project.IdentityService.Data;
using Project.IdentityService.Data.Configurations;

namespace Project.IdentityService.Repository.RoleRepository
{
    public class RoleRepository : MSSQLRepository<ApplicationDbContext, Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
