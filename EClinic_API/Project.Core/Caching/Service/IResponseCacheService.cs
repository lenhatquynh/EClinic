using Microsoft.AspNetCore.Mvc;

namespace Project.Core.Caching.Service
{
    public interface IResponseCacheService
    {
        Task SetCacheResponseAsync(string cacheKey,object response,TimeSpan timeOut);
        Task<string> GetCacheResponseAsync(string cacheKey);
        Task RemoveCacheAsync(string cacheKey);
    }
}
