using Orders.Domain.Enums;
using Shared.Domain.Exceptions;

namespace Orders.Domain.Exceptions
{
    public sealed class InvalidPaymentStatusTransitionException : DomainException
    {
        public InvalidPaymentStatusTransitionException(
            OrderPaymentStatus current,
            OrderPaymentStatus attempted) : base($"Cannot transition payment status from '{current}' to '{attempted}'."){ }
    }
}
