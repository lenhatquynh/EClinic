using Microsoft.AspNetCore.Mvc.Filters;
using Project.Common.Response;
using Project.Data.Repository.MSSQL;

namespace Project.Core.Filters
{
    public class NotFoundIdFilter<TRepository, TEntity> : IAsyncActionFilter where TRepository : IMSSQLRepository<TEntity> where TEntity : class
    {

        private readonly TRepository repository;

        public NotFoundIdFilter(TRepository repository)
        {
            this.repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var idValue = context.ActionArguments.Values.FirstOrDefault().GetType().GUID;

            if (idValue == Guid.Empty)
            {
                await next.Invoke();
                return;
            }
            var anyEntity = await repository.AnyAsync(idValue);
            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result = ApiResponse.NotFound($"{typeof(TEntity).Name} not found");

        }
    }
}
