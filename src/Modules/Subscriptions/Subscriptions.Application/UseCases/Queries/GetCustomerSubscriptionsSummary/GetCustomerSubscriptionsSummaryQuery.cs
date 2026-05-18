using MediatR;
using Shared.Application;

namespace Subscriptions.Application.UseCases.Queries.GetCustomerSubscriptionsSummary
{
    public sealed record GetCustomerSubscriptionsSummaryQuery(
        Guid CustomerId,
        string? Status,
        bool SortDescending,
        int Page,
        int PageSize) : IRequest<PagedResult<CustomerSubscriptionSummary>>;

    public sealed record CustomerSubscriptionSummary(
        Guid SubscriptionId,
        Guid RestaurantId,
        string RestaurantName,
        string RestaurantLogoUrl,
        DateOnly? NextDelivery,
        DateOnly StartDate,
        int TotalItems,
        decimal TotalPriceAmount,
        string PriceCurrency,
        string Status);
}
