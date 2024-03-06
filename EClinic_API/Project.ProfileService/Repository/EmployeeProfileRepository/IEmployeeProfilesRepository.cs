using Project.Data.Repository.MSSQL;
using Project.ProfileService.Data;

namespace Project.ProfileService.Repository.EmployeeProfileRepository
{
    public interface IEmployeeProfilesRepository : IMSSQLRepository<EmployeeProfile>
    {
    }
}
