using Project.Data.Repository.MSSQL;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Data.Configurations;

namespace Project.ServiceInformationService.Repository.SpecializationRepository;

public class SpecializationRepository : MSSQLRepository<ApplicationDbContext, Specialization>, ISpecializationRepository
{
    public SpecializationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
