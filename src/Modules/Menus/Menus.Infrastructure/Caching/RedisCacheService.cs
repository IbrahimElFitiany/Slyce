using Menus.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Menus.Infrastructure.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task ClearAsync(string key, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);
        }
    }
}
