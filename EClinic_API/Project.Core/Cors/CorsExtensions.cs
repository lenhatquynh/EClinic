using Microsoft.Extensions.DependencyInjection;

namespace Project.Core.Cors
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddMyCors(this IServiceCollection services, string CorsName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsName, builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination")
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials());
            });
            return services;
        }
    }
}
