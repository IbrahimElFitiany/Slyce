using Shared.Domain.Common;
using Shared.Domain.ValueObjects;

namespace Orders.Domain.Aggregates.Order
{
    public sealed class OrderItem : Entity
    {
        public Guid MealId { get; }
        public Guid SizeId { get; }
        public int Quantity { get; }
        public Price UnitPrice { get; } = null!;
        public Price TotalPrice { get; } = null!;

        private OrderItem() { }

        internal OrderItem(Guid mealId, Guid sizeId, int quantity, Price unitPrice)
        {
            MealId = mealId;
            SizeId = sizeId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = unitPrice * quantity;
        }
    }
}
