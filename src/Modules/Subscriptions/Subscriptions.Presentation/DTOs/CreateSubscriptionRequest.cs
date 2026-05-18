namespace Subscriptions.Presentation.DTOs
{
    public sealed record CreateSubscriptionRequest(
        Guid BranchId,
        Guid DeliveryAddressId,
        IReadOnlyCollection<DayOfWeek> DeliveryDays,
        IReadOnlyCollection<SubscriptionMealRequest> SubscriptionMeals,
        string TimeSlot,
        DateOnly StartDate,
        string BillingCycle);

    public sealed record SubscriptionMealRequest(Guid MealSizeId,int Quantity);
}

