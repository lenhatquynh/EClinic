using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Project.Data.Repository.MSSQL
{
    public class MSSQLRepository<TDbContext, TEntity> : IMSSQLRepository<TEntity> where TDbContext : DbContext where TEntity : class
    {
        private readonly TDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;
        public MSSQLRepository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Set<TEntity>();
        }
        public async Task<bool> AnyAsync(Guid ID)
        {
            var result = await dbSet.FindAsync(ID);
            return result != null;
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filters)
        {
            return await dbSet.AnyAsync(filters);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filters)
        {
            return await dbSet.CountAsync(filters);
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            var result = await dbContext.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<TEntity> CreateEntityAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            var result = await dbContext.SaveChangesAsync();
            var check = Convert.ToBoolean(result);
            if (check)
            {
                return entity;
            }
            return null;
            
        }

        public async Task<bool> CreateRangeAsync(List<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
            var result = await dbContext.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            dbSet.Remove(entity);
            var result = await dbContext.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<bool> DeleteRangeAsync(List<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            var result = await dbContext.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> filters)
        {
            var entities = await GetAllAsync(filters);
            return await DeleteRangeAsync(entities);
        }

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filters)
        {
            return Task.FromResult(dbSet.Where(filters).ToList());
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return Task.FromResult(dbSet.ToList());
        }

        public async Task<TEntity> GetAsync(Guid ID)
        {
            return await dbSet.FindAsync(ID);
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filters)
        {
            return await dbSet.FirstOrDefaultAsync(filters);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            dbSet.Update(entity);
            var result = await dbContext.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<bool> UpdateRangeAsync(List<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
            var result = await dbContext.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }
    }
}
