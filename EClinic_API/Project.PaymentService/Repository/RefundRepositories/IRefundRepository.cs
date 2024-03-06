using Project.Data.Repository.MSSQL;
using Project.PaymentService.Data;
using System.Linq.Expressions;

namespace Project.PaymentService.Repository.RefundRepositories
{
    public interface IRefundRepository : IMSSQLRepository<Refund>
    {
        Task<List<Refund>> GetAllRefund();
        Task<List<Refund>> GetAllRefund(Expression<Func<Refund, bool>> filters);
        Task<Refund> GetRefund(Guid RefundID);
        Task<Refund> GetRefund(Expression<Func<Refund, bool>> filter);
    }
}
