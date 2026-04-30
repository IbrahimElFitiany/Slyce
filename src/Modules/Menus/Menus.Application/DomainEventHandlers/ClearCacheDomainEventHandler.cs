using MediatR;
using Menus.Application.Interfaces;
using Menus.Domain.DomainEvents;

namespace Menus.Application.DomainEventHandlers
{
    public class ClearCacheDomainEventHandler : 
        INotificationHandler<MealSizeAddedDomainEvent>,
        INotificationHandler<MealSizeRemovedDomainEvent>
    {
        private readonly ICacheService _cache;

        public ClearCacheDomainEventHandler (ICacheService cacheService) { 
        
            _cache = cacheService;
        }

        public async Task Handle(MealSizeAddedDomainEvent notification, CancellationToken ct)
        {
            await _cache.ClearAsync($"meal:{notification.MealId}", ct);
        }

        public async Task Handle(MealSizeRemovedDomainEvent notification, CancellationToken ct)
        {
            await _cache.ClearAsync($"meal:{notification.MealId}", ct);
        }
    }
}
