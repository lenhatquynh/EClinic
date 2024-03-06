using Microsoft.EntityFrameworkCore;
using Project.Data.Repository.MSSQL;
using Project.PaymentService.Data;
using Project.PaymentService.Data.Configurations;
using System.Linq.Expressions;

namespace Project.PaymentService.Repository.RefundRepositories
{
    public class RefundRepository : MSSQLRepository<ApplicationDbContext, Refund>, IRefundRepository
    {
        private readonly ApplicationDbContext context;
        public RefundRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Refund>> GetAllRefund()
        {
            return await context.Refunds.Include(x => x.Payment).ToListAsync();
        }

        public async Task<List<Refund>> GetAllRefund(Expression<Func<Refund, bool>> filters)
        {
            return await context.Refunds.Include(x => x.Payment).Where(filters).ToListAsync();
        }

        public async Task<Refund> GetRefund(Guid RefundID)
        {
            return await context.Refunds.Include(x => x.Payment).SingleOrDefaultAsync(x => x.RefundID == RefundID);
        }

        public async Task<Refund> GetRefund(Expression<Func<Refund, bool>> filter)
        {
            return await context.Refunds.Include(x => x.Payment).SingleOrDefaultAsync(filter);
        }
    }
}
