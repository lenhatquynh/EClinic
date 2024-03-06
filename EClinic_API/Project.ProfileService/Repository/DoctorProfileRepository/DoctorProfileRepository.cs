using Project.Data.Repository.MSSQL;
using Project.ProfileService.Data;
using Project.ProfileService.Data.Configurations;

namespace Project.ProfileService.Repository.DoctorProfileRepository
{
    public class DoctorProfileRepository : MSSQLRepository<ApplicationDbContext, DoctorProfile>, IDoctorProfileRepository
    {
        public DoctorProfileRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
} 