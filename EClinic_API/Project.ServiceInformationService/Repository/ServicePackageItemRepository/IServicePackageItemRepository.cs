using Project.Data.Repository.MSSQL;
using Project.ServiceInformationService.Data;

namespace Project.ServiceInformationService.Repository.ServicePackageItemRepository;

public interface IServicePackageItemRepository : IMSSQLRepository<ServicePackageItem>
{
}
