using Shared.Domain.Common;

namespace Menus.Domain.DomainEvents
{
    public sealed record MealSizeAddedDomainEvent(Guid MealId, Guid SizeId) : IDomainEvent;
}
