using Microsoft.EntityFrameworkCore;
using Project.Data.Repository.MSSQL;
using Project.PaymentService.Data;
using Project.PaymentService.Data.Configurations;
using System.Linq.Expressions;

namespace Project.PaymentService.Repository.PaymentRepositories
{
    public class PaymentRepository : MSSQLRepository<ApplicationDbContext, Payment>, IPaymentRepository
    {
        private readonly ApplicationDbContext context;
        public PaymentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Payment>> GetAllPayment()
        {
            return await context.Payments.Include(x => x.Refund).ToListAsync();
        }

        public async Task<List<Payment>> GetAllPayment(Expression<Func<Payment, bool>> filters)
        {
            return await context.Payments.Include(x => x.Refund).Where(filters).ToListAsync();
        }

        public async Task<Payment> GetPayment(Guid PaymentID)
        {
            return await context.Payments.Include(x => x.Refund).SingleOrDefaultAsync(x => x.PaymentID == PaymentID);
        }

        public async Task<Payment> GetPayment(Expression<Func<Payment, bool>> filter)
        {
            return await context.Payments.Include(x => x.Refund).SingleOrDefaultAsync(filter);
        }
    }
}
