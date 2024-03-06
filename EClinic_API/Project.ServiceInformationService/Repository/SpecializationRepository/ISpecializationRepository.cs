using Project.Data.Repository.MSSQL;
using Project.ServiceInformationService.Data;

namespace Project.ServiceInformationService.Repository.SpecializationRepository;

public interface ISpecializationRepository : IMSSQLRepository<Specialization>
{
}
