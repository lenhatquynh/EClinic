using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Project.Core.Caching.Service;
using System.Text;

namespace Project.Core.Caching.Attributes
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int timeToLiveSeconds;

        public CacheAttribute(int timeToLiveSeconds)
        {
            this.timeToLiveSeconds = timeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cacheKey = GenerateCacheKeyFromRequest(context);
            var cacheResponse = await cacheService.GetCacheResponseAsync(cacheKey);
            if (!string.IsNullOrEmpty(cacheResponse))
            {
                context.Result = new ObjectResult("Response")
                {
                    StatusCode = StatusCodes.Status200OK,
                    Value = cacheResponse
                }; ;
                return;
            }
            var executeContext = await next();
            if (executeContext.Result is ObjectResult objectResult)
            {
                await cacheService.SetCacheResponseAsync(cacheKey, objectResult.Value, TimeSpan.FromSeconds(timeToLiveSeconds));
            }
        }

        private static string GenerateCacheKeyFromRequest(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Method}-{request.Path}");
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"{request.Method}-{request.Path}-{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}