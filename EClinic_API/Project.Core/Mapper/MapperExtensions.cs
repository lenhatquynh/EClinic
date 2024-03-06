using Microsoft.Extensions.DependencyInjection;


namespace Project.Core.Mapper
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMyMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
