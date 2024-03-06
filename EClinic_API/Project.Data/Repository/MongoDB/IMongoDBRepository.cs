using Project.Data.Model;
using System.Linq.Expressions;

namespace Project.Data.Repository.MongoDB
{
    public interface IMongoDBRepository<T> where T : MongoDBEntity
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filters);
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task CreateAsync(T entity);
        Task CreateManyAsync(List<T> entity);
        Task UpdateAsync(T entity);
        Task UpdateManyAsync(List<T> entities);
        Task RemoveAsync(Guid id);
        Task RemoveAsync(Expression<Func<T, bool>> filter);
        Task RemoveManyAsync(List<Guid> id);
        Task RemoveManyAsync(Expression<Func<T, bool>> filters);
    }
}
