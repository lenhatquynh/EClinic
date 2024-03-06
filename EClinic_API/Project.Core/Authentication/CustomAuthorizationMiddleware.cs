using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Project.Core.Caching.Service;

namespace Project.Core.Authentication
{
    public class CustomAuthorizationMiddleware : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            //var cacheService = context.RequestServices.GetRequiredService<IResponseCacheService>();
            //if (authorizeResult.Succeeded)
            //{
            //    string userId = context.User.Claims.FirstOrDefault(claim => claim.Type == "UserID").Value;
            //    var result = await cacheService.GetCacheResponseAsync(userId);
            //    if (result != null)
            //    {
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //        return;
            //    }

            //}
            await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
