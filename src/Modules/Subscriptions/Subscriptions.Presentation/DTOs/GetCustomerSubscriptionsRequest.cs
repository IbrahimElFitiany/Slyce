namespace Subscriptions.Presentation.DTOs
{
    public sealed record GetCustomerSubscriptionsRequest(
        string? Status,
        bool SortDescending = true,
        int Page = 1,
        int PageSize = 10);
}