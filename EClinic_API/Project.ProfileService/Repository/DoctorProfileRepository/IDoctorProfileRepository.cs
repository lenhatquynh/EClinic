using Project.Data.Repository.MSSQL;
using Project.ProfileService.Data;

namespace Project.ProfileService.Repository.DoctorProfileRepository
{
    public interface IDoctorProfileRepository : IMSSQLRepository<DoctorProfile>
    {
    }
}
