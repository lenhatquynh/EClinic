using Project.Data.Repository.MSSQL;
using Project.IdentityService.Data;

namespace Project.IdentityService.Repository.RoleRepository
{
    public interface IRoleRepository : IMSSQLRepository<Role>
    {
    }
}
