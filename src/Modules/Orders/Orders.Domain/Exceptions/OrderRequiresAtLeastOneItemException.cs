using Shared.Domain.Exceptions;

namespace Orders.Domain.Exceptions
{
    public sealed class OrderRequiresAtLeastOneItemException : DomainException
    {
        public OrderRequiresAtLeastOneItemException() : base("An order must contain at least one item.") { }
    }
}
