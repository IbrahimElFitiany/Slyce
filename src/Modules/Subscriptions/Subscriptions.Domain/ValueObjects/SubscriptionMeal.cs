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
            ArgumentOutOfRangeException.ThrowIfEqual(MealId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfEqual(SizeId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(Quantity, 0);

            MealId = mealId;
            SizeId = sizeId;
            Quantity = quantity;
            PriceAtSubscription = priceAtSubscription;
        }
    
    }
}
