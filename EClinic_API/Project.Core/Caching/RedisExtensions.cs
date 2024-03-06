using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Core.Caching.Service;
using Project.Core.Model;
using StackExchange.Redis;

namespace Project.Core.Caching
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services,IConfiguration configuration)
        {
            RedisConfiguration redisConfigugation = configuration.GetSection("Redis").Get<RedisConfiguration>();
            services.AddSingleton(redisConfigugation);
            services.AddSingleton<IConnectionMultiplexer>(_=>ConnectionMultiplexer.Connect(redisConfigugation.Host));
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConfigugation.Host;
            });
            services.AddSingleton<IResponseCacheService,ResponseCacheService>();
            return services;
        }
    }
}
