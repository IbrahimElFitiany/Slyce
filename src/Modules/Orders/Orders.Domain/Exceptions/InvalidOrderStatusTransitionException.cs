using Orders.Domain.Enums;
using Shared.Domain.Exceptions;

namespace Orders.Domain.Exceptions
{
    public sealed class InvalidOrderStatusTransitionException : DomainException
    {
        public InvalidOrderStatusTransitionException(
            OrderStatus current,
            OrderStatus attempted) : base($"Cannot transition order status from '{current}' to '{attempted}'."){ }
    }
}
