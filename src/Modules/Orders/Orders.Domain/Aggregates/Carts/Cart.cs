using Orders.Domain.Exceptions;
using Shared.Domain.Common;

namespace Orders.Domain.Aggregates.Carts
{
    public sealed class Cart : AggregateRoot
    {
        public Guid CustomerId { get; private init; }
        public Guid BranchId { get; private init; }

        private readonly List<CartItem> _cartItems = [];
        public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

        private Cart() { }

        public Cart(Guid customerId, Guid branchId) {

            ArgumentOutOfRangeException.ThrowIfEqual(customerId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfEqual(branchId, Guid.Empty);

            Id = Guid.NewGuid();
            CustomerId = customerId;
            BranchId = branchId;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public void AddCartItem(Guid mealId, Guid sizeId, int quantity, Guid branchId)
        {
            if (branchId != BranchId)
                throw new CartBranchMismatchException();

            var cartItem = _cartItems.FirstOrDefault(ci => ci.MealId == mealId && ci.SizeId == sizeId);

            if (cartItem is not null)
            {
                cartItem.IncreaseQuantity(quantity);
                return;
            }

            _cartItems.Add(new CartItem(mealId, sizeId, quantity));
        }

        public void RemoveCartItem(Guid mealId)
        {
            var cartItem = _cartItems.FirstOrDefault(ci => ci.MealId == mealId)
                ?? throw new Exception();

            _cartItems.Remove(cartItem);
        }

        public void UpdateCartItemQuantity(Guid mealId, Guid sizeId, int quantity)
        {
            var cartItem = _cartItems.FirstOrDefault(ci => ci.MealId == mealId && ci.SizeId == sizeId)
                ?? throw new CartItemNotFoundException();

            if (quantity == 0)
            {
                _cartItems.Remove(cartItem);
                return;
            }

            if (quantity > cartItem.Quantity)
                cartItem.IncreaseQuantity(quantity - cartItem.Quantity);

            else
                cartItem.DecreaseQuantity(cartItem.Quantity - quantity);
        }

        public void RemoveCartItem(Guid mealId, Guid sizeId)
        {
            var cartItem = _cartItems.FirstOrDefault(ci => ci.MealId == mealId && ci.SizeId == sizeId)
                ?? throw new CartItemNotFoundException();

            _cartItems.Remove(cartItem);
        }

    }
}
