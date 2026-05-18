namespace Restaurants.Presentation.DTOs
{
    public sealed record GetRestaurantApplicationsSummaryRequest(
        string? Status,
        bool SortDescending = true,
        DateOnly? From = null,
        DateOnly? To = null,
        int Page = 1,
        int PageSize = 20);
}