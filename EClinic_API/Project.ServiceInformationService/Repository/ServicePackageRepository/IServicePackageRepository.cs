using Project.Data.Repository.MSSQL;
using Project.ServiceInformationService.Data;
using System.Linq.Expressions;

namespace Project.ServiceInformationService.Repository.ServicePackageRepository;

public interface IServicePackageRepository : IMSSQLRepository<ServicePackage>
{
    Task<List<ServicePackage>> GetAllServicePackageAsync(Expression<Func<ServicePackage, bool>> filters);
    Task<ServicePackage> GetServicePackageAsync(Expression<Func<ServicePackage, bool>> filters);

}
