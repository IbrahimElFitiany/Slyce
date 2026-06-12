using Shared.Domain.Exceptions;

namespace Orders.Domain.Exceptions
{
    public sealed class CartItemNotFoundException : DomainException
    {
        public CartItemNotFoundException()
            : base("Cart item was not found.") { }
    }
}