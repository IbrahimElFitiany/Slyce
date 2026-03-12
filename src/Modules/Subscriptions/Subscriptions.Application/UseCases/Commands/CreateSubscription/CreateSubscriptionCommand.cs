using MediatR;

namespace Subscriptions.Application.UseCases.Commands.CreateSubscription
{
    public sealed record CreateSubscriptionCommand(
        Guid CustomerId,
        Guid BranchId,
        Guid DeliveryAddressId,
        string TimeSlot,
        IEnumerable<DayOfWeek> SubscriptionDays,
        DateOnly StartDate,
        IEnumerable<SubscriptionMealInput> SubscriptionMeals,
        string BillingCycle):IRequest<Guid>;

    public sealed record SubscriptionMealInput(Guid SizeId, int Quantity);
}
