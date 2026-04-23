using Shared.Domain.Common;

namespace Orders.Domain.Aggregates.Order
{
    public sealed class OrderItem : Entity
    {
        public Guid MealId { get; }
        public Guid SizeId { get; }
        public int Quantity { get; }
        public decimal Price { get; }

        private OrderItem() { }
        internal OrderItem(Guid mealId, Guid sizeId, int quantity, decimal price)
        {
            MealId = mealId;
            SizeId = sizeId;
            Quantity = quantity;
            Price = price;
        }
    }
}
