using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Core.MediatR
{
    public static class MediatRExtentions
    {
        public static IServiceCollection AddMyMediatR(this IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            return services;
        }
    }
}
