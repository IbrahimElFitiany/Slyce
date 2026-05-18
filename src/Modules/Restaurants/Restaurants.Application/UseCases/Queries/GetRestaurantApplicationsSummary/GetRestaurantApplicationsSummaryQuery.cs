using MediatR;
using Shared.Application;

namespace Restaurants.Application.UseCases.Queries.GetRestaurantApplicationsSummary
{
    public sealed record GetRestaurantApplicationsSummaryQuery(
        string? Status,
        bool SortDescending,
        DateOnly? From,
        DateOnly? To,
        int Page,
        int PageSize) : IRequest<PagedResult<RestaurantApplicationResult>>;

    public sealed record RestaurantApplicationResult(
        Guid ApplicationId,
        string BrandName,
        string Status,
        DateTime CreatedAt);
}
