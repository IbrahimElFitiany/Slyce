using Shared.Domain.Common;
using Shared.Domain.ValueObjects;
using Subscriptions.Domain.Enums;
using Subscriptions.Domain.Exceptions;
using Subscriptions.Domain.ValueObjects;

namespace Subscriptions.Domain.Entities
{
    public sealed class Subscription : AggregateRoot
    {
        public Guid CustomerId { get; private init; }
        public Guid BranchId { get; private init; }
        public Guid DeliveryAddressId{ get; private init; }
        public Address DeliveryAddress { get; private set; } = null!;
        public DeliveryTimeFrame TimeFrame { get; private set; } = null!;

        private readonly List<SubscriptionDeliveryDay> _deliveryDays = [];
        public IReadOnlyCollection<SubscriptionDeliveryDay> DeliveryDays { get; private set; } = null!;
        
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }

        private readonly List<SubscriptionMeal> _subscriptionMeals = [];
        public IReadOnlyCollection<SubscriptionMeal> SubscriptionMeals => _subscriptionMeals;
        
        public Price TotalPrice { get; private set; } = null!;
        public BillingCycle BillingCycle { get; private set; }
        public SubscriptionStatus Status { get; private set; }

        private Subscription() { }

        public Subscription (
            Guid customerId,
            Guid branchId,
            Guid deliveryAddressId,
            Address deliveryAddress,
            DeliveryTimeFrame timeFrame,
            IEnumerable<SubscriptionDeliveryDay> deliveryDays,
            DateOnly startDate,
            IEnumerable<SubscriptionMeal> subscriptionMeals,
            BillingCycle billingCycle
)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(customerId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfEqual(branchId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfEqual(deliveryAddressId, Guid.Empty);
            ArgumentNullException.ThrowIfNull(timeFrame, nameof(timeFrame));

            Id = Guid.NewGuid();
            CustomerId = customerId;
            BranchId = branchId;
            DeliveryAddressId = deliveryAddressId;
            DeliveryAddress = deliveryAddress;
            TimeFrame = timeFrame;

            ValidateDeliveryDaysCount(deliveryDays);
            _deliveryDays.AddRange(deliveryDays);

            ValidateSubscriptionMealsCount(subscriptionMeals);
            _subscriptionMeals.AddRange(subscriptionMeals);

            if (startDate < DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidSubscriptionDateException(startDate);

            StartDate = startDate;
            EndDate = billingCycle switch
            {
                BillingCycle.Weekly => startDate.AddDays(7),
                BillingCycle.Monthly => startDate.AddDays(30),
                _ => throw new ArgumentOutOfRangeException(nameof(billingCycle))
            };

            TotalPrice = CalculatePriceFromSubscriptionMeals(StartDate, EndDate, _deliveryDays, _subscriptionMeals);
            Status = SubscriptionStatus.Active;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }


        private static void ValidateDeliveryDaysCount(IEnumerable<SubscriptionDeliveryDay> deliveryDays)
        {
            if (!deliveryDays.Any())
                throw new SubscriptionHasNoDeliveryDaysException();
        }
        private static void ValidateSubscriptionMealsCount(IEnumerable<SubscriptionMeal> subscriptionMeals)
        {
            if (!subscriptionMeals.Any())
                throw new EmptySubscriptionMealsException();
        }
        private static Price CalculatePriceFromSubscriptionMeals(DateOnly startDate, DateOnly endDate,IEnumerable<SubscriptionDeliveryDay> deliveryDays, IEnumerable<SubscriptionMeal> subscriptionMeals)
        {
            var dailyPrice = subscriptionMeals.Aggregate(Price.EGP(0), (total, meal) => total + meal.PriceAtSubscription);
            var totalDeliveries = CountDeliveries(startDate, endDate, deliveryDays);

            return dailyPrice * totalDeliveries;
        }
        private static int CountDeliveries(DateOnly startDate, DateOnly endDate, IEnumerable<SubscriptionDeliveryDay> deliveryDays)
        {
            var days = deliveryDays.Select(d => d.Day).ToHashSet();
            int counter = 0;

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (days.Contains(date.DayOfWeek))
                    counter++;
            }

            return counter;
        }
    }
}
