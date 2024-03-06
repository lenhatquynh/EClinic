using Project.Data.Repository.MSSQL;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Data.Configurations;

namespace Project.ServiceInformationService.Repository.ServicePackageItemRepository;

public class ServicePackageItemRepository : MSSQLRepository<ApplicationDbContext, ServicePackageItem>, IServicePackageItemRepository
{
    public ServicePackageItemRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
