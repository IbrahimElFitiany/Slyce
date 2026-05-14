using Shared.Domain.ValueObjects;

namespace Subscriptions.Domain.ValueObjects
{
    public sealed record SubscriptionMeal 
    {
        public Guid MealId { get; }
        public Guid SizeId { get; }
        public int Quantity { get; }
        public Price PriceAtSubscription { get; } = null!;


        private SubscriptionMeal() { }

        public SubscriptionMeal(
            Guid mealId,
            Guid sizeId,
            int quantity,
            Price priceAtSubscription)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(mealId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfEqual(sizeId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(quantity, 0);

            MealId = mealId;
            SizeId = sizeId;
            Quantity = quantity;
            PriceAtSubscription = priceAtSubscription;
        }
    
    }
}
