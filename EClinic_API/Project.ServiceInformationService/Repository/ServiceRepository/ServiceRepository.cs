using Microsoft.EntityFrameworkCore;
using Project.Data.Repository.MSSQL;
using Project.ServiceInformationService.Data.Configurations;
using System.Linq.Expressions;

namespace Project.ServiceInformationService.Repository.ServiceRepository;

public class ServiceRepository : MSSQLRepository<ApplicationDbContext, Data.Service>, IServiceRepository
{
    private readonly ApplicationDbContext context;
    public ServiceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        this.context = dbContext;
    }

    public async Task<List<Data.Service>> GetAllServiceAsync(Expression<Func<Data.Service, bool>> filters)
    {
        var result = await context.Services.Include(x => x.Specialization).Where(filters).ToListAsync();
        return result;
    }

    public async Task<Data.Service> GetServiceAsync(Expression<Func<Data.Service, bool>> filters)
    {
        var result = await context.Services.Include(x => x.Specialization).FirstOrDefaultAsync(filters);
        return result;
    }
}
