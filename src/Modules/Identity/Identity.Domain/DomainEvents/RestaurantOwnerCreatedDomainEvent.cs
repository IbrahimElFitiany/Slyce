using Shared.Domain.Common;

namespace Identity.Domain.DomainEvents
{
    public sealed record RestaurantOwnerCreatedDomainEvent(Guid UserId, string Email) : IDomainEvent{ }
}