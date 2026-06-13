using MediatR;
using Restaurants.Domain.Enums;
using Shared.Application;

namespace Restaurants.Application.UseCases.Queries.GetRestaurants
{
    public sealed record GetRestaurantsQuery(
        RestaurantStatus? Status,
        int Page,
        int PageSize
    ) : IRequest<PagedResult<RestaurantSummaryResponse>>;

    public sealed record RestaurantSummaryResponse(
        Guid Id,
        string Name,
        Guid OwnerId,
        string Status,
        string City,
        DateTimeOffset CreatedAt);
}
