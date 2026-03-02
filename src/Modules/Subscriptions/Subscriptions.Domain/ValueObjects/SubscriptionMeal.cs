using Shared.Domain.ValueObjects;

namespace Subscriptions.Domain.ValueObjects
{
    public sealed record SubscriptionMeal 
    {
        public Guid MealId { get; }
        public Guid SizeId { get; }
        public int Quantity { get; }
        public Price PriceAtSubscription { get; } = null!;



        public SubscriptionMeal()
        {
            ArgumentOutOfRangeException.ThrowIfEqual(MealId,Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfEqual(SizeId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(Quantity, 0);
        }
    
    }
}
