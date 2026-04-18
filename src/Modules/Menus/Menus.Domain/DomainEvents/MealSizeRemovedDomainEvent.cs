using Shared.Domain.Common;

namespace Menus.Domain.DomainEvents
{
    public sealed record MealSizeRemovedDomainEvent(Guid MealId, Guid SizeId) : IDomainEvent;
}
