using Project.Data.Repository.MSSQL;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Service;
using System.Linq.Expressions;

namespace Project.ServiceInformationService.Repository.ServiceRepository;

public interface IServiceRepository : IMSSQLRepository<Data.Service>
{
    Task<List<Data.Service>> GetAllServiceAsync(Expression<Func<Data.Service, bool>> filters);
    Task<Data.Service> GetServiceAsync(Expression<Func<Data.Service, bool>> filters);

}
