using Identity.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Identity.Infrastructure.Cache
{
    internal sealed class RedisCache(IDistributedCache cache) : ICacheService
    {

        public async Task<string?> GetAsync(string key, CancellationToken ct) => await cache.GetStringAsync(key, ct);

        public async Task RemoveAsync(string key, CancellationToken cancellationToken)
        {
            await cache.RemoveAsync(key, cancellationToken);
        }

        public async Task SetAsync (string key, string value, CancellationToken cancellationToken)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3)
            };

            await cache.SetStringAsync(key, value, options, cancellationToken);
        }
    }
}