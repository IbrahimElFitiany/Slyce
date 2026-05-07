using Shared.Domain.Common;

namespace Identity.Domain.DomainEvents
{
    public sealed record CustomerCreatedDomainEvent(Guid CustomerId, string Email) : IDomainEvent{ }
}