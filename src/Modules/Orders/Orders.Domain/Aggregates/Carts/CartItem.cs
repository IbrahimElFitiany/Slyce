using Orders.Domain.Exceptions;
using Shared.Domain.Common;

namespace Orders.Domain.Aggregates.Carts
{
    public sealed class CartItem : Entity
    {
        public Guid MealId { get; private init; }
        public Guid SizeId { get; private init; }
        public int Quantity { get; private set; }

        private CartItem() { }

        internal CartItem(Guid mealId, Guid sizeId, int quantity)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(mealId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfEqual(sizeId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            Id = Guid.NewGuid();
            MealId = mealId;
            SizeId = sizeId;
            Quantity = quantity;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        internal void IncreaseQuantity(int by = 1)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(by);
            Quantity += by;
        }

        internal void DecreaseQuantity(int by = 1)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(by);

            if (Quantity - by < 1)
                throw new CartItemQuantityException();

            Quantity -= by;
        }

    }
}