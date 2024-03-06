using Project.Data.Repository.MSSQL;
using Project.ProfileService.Data;

namespace Project.ProfileService.Repository.HealthProfileRepository
{
    public interface IHealthProfileRepository : IMSSQLRepository<HealthProfile>
    {
    }
}
