using MediatR;

namespace Restaurants.Application.UseCases.Queries.GetNearbyTopRatedRestaurants
{
    public sealed record GetNearbyTopRatedRestaurantsQuery(double Latitude , double Longitude): IRequest<IReadOnlyCollection<NearbyRestaurant>>;

    public sealed record NearbyRestaurant(
        Guid BranchId,
        string RestaurantName,
        string? RestaurantLogoUrl,
        float Rating,
        string? RestaurantBanner);
}