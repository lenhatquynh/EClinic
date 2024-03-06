using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;

namespace Project.Core.Caching.Service
{
    internal class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache distributedCache;
        private readonly IConnectionMultiplexer connectionMultiplexer;

        public ResponseCacheService(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer)
        {
            this.distributedCache = distributedCache;
            this.connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<string> GetCacheResponseAsync(string cacheKey)
        {
            var cacheResponse = await distributedCache.GetStringAsync(cacheKey);
            if (string.IsNullOrEmpty(cacheResponse))
            {
                return null;
            }

            return cacheResponse;
        }

        public async Task RemoveCacheAsync(string cacheKey)
        {
            await distributedCache.RemoveAsync(cacheKey);
        }

        public async Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut)
        {
            if (response == null)
            {
                return;
            }
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var sirializerResponse = JsonConvert.SerializeObject(response, settings);
            await distributedCache.SetStringAsync(cacheKey, sirializerResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeOut
            });
        }
    }
}