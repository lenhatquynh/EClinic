using Project.Data.Repository.MSSQL;
using Project.PaymentService.Data;
using System.Linq.Expressions;

namespace Project.PaymentService.Repository.PaymentRepositories
{
    public interface IPaymentRepository : IMSSQLRepository<Payment>
    {
        Task<List<Payment>> GetAllPayment();
        Task<List<Payment>> GetAllPayment(Expression<Func<Payment, bool>> filters);
        Task<Payment> GetPayment(Guid PaymentID);
        Task<Payment> GetPayment(Expression<Func<Payment, bool>> filter);
    }
}
