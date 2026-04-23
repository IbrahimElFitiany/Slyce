using Shared.Domain.Common;

namespace Orders.Domain.DomainEvents
{
    public sealed record OrderStatusChangedDomainEvent(Guid OrderId, Guid CustomerId, string Status) : IDomainEvent;
}
