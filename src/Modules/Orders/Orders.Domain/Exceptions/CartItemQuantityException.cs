using Shared.Domain.Exceptions;

namespace Orders.Domain.Exceptions
{
    public sealed class CartItemQuantityException : DomainException
    {
        public CartItemQuantityException() : base("Quantity cannot drop below 1. Remove the item instead.") { }
        public CartItemQuantityException(int quantity, int by) : base($"Cannot decrease quantity by '{by}', current quantity is '{quantity}'.") { }
    }
}