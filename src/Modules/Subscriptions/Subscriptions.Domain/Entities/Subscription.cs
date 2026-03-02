using Shared.Domain.ValueObjects;
using Subscriptions.Domain.Enums;
using Subscriptions.Domain.Exceptions;
using Subscriptions.Domain.ValueObjects;

namespace Subscriptions.Domain.Entities
{
    public sealed class Subscription
    {
        public Guid Id { get; private init; }
        public Guid CustomerId { get; private init; }
        public Guid BranchId { get; private init; }
        public Guid DeliveryAddressId{ get; private init; }
        public DeliveryTimeFrame TimeFrame { get; private set; } = null!;

        private List<SubscriptionDeliveryDay> _deliveryDays = new();
        public IReadOnlyCollection<SubscriptionDeliveryDay> DeliveryDays { get; private set; } = null!;
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }

        private List<SubscriptionMeal> _subscriptionMeals = new();
        public IReadOnlyCollection<SubscriptionMeal> SubscriptionMeals => _subscriptionMeals;
        public Price TotalPrice { get; private set; } = null!;
        public BillingCycle BillingCycle { get; private set; }
        public SubscriptionStatus Status { get; private set; }
        public DateTime CreatedAt { get; private init; }
        public DateTime UpdatedAt { get; private set; }
        private Subscription() { }

        public Subscription (
            Guid customerId,
            Guid branchId,
            Guid deliveryAddressId,
            IEnumerable<SubscriptionDeliveryDay> deliveryDays,
            DeliveryTimeFrame timeFrame,
            IEnumerable<SubscriptionMeal> subscriptionMeals,
            BillingCycle billingCycle,
            DateOnly startDate)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(customerId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfEqual(branchId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfEqual(deliveryAddressId, Guid.Empty);
            ArgumentNullException.ThrowIfNull(timeFrame, nameof(timeFrame));

            Id = Guid.NewGuid();
            CustomerId = customerId;
            BranchId = branchId;
            DeliveryAddressId = deliveryAddressId;

            ValidateDeliveryDaysCount(deliveryDays);
            _deliveryDays.AddRange(deliveryDays);

            ValidateSubscriptionMealsCount(subscriptionMeals);
            _subscriptionMeals.AddRange(subscriptionMeals);

            StartDate = startDate;
            EndDate = billingCycle switch
            {
                BillingCycle.Weekly => startDate.AddDays(7),
                BillingCycle.Monthly => startDate.AddMonths(1),
                _ => throw new ArgumentOutOfRangeException(nameof(billingCycle))
            };
            TotalPrice = CalculatePriceFromSubscriptionMeals(SubscriptionMeals);
            Status = SubscriptionStatus.Active;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }


        private static void ValidateSubscriptionMealsCount(IEnumerable<SubscriptionMeal> subscriptionMeals)
        {
            if (!subscriptionMeals.Any())
                throw new EmptySubscriptionMealsException();
        }
        private static void ValidateDeliveryDaysCount(IEnumerable<SubscriptionDeliveryDay> deliveryDays)
        {
            if (!deliveryDays.Any())
                throw new EmptySubscriptionMealsException();
        }
        private static Price CalculatePriceFromSubscriptionMeals(IEnumerable<SubscriptionMeal> subscriptionMeals)
        {
            Price price = Price.EGP(0);

            foreach (var meal in subscriptionMeals)
            {
                price = price + meal.PriceAtSubscription;
            }

            return price;
        }

    }
}
