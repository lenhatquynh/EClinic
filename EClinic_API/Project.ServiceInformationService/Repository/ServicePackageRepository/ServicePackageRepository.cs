using Microsoft.EntityFrameworkCore;
using Project.Data.Repository.MSSQL;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Data.Configurations;
using System.Linq.Expressions;

namespace Project.ServiceInformationService.Repository.ServicePackageRepository;

public class ServicePackageRepository : MSSQLRepository<ApplicationDbContext, ServicePackage>, IServicePackageRepository
{
    private readonly ApplicationDbContext context;
    public ServicePackageRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        this.context = dbContext;
    }

    public async Task<List<ServicePackage>> GetAllServicePackageAsync(Expression<Func<ServicePackage, bool>> filters)
    {
        var result = await context.ServicePackages.Include(x => x.ServicePackageItems)
                                .ThenInclude(servicePackageItem => servicePackageItem.Service)
                                .ThenInclude(service => service.Specialization)
                                .Where(filters).ToListAsync();

        return result;
    }

    public async Task<ServicePackage> GetServicePackageAsync(Expression<Func<ServicePackage, bool>> filters)
    {
        var result = await context.ServicePackages.Include(x => x.ServicePackageItems)
                                .ThenInclude(servicePackageItem => servicePackageItem.Service)
                                .ThenInclude(service => service.Specialization)
                                .FirstOrDefaultAsync(filters);

        return result;
    }

}
