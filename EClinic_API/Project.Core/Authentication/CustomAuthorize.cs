using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Project.Common.Response;
using System.Security.Claims;

namespace Project.Core.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CustomAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {
        public string[] Authorities { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var roles = context.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            //no Authorize
            if (Authorities == null || Authorities.Length == 0)
                return;

            //token not send
            if (roles.Count == 0)
            {
                context.Result = ApiResponse.Forbidden();
            }

            var checkRoles = Authorities.Intersect(roles);
            if (!checkRoles.Any())
            {
                context.Result = ApiResponse.Forbidden();
            }
        }
    }
}
