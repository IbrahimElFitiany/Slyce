using MediatR;
using Restaurants.Application.UseCases.Queries.GetNearbyTopRatedRestaurants;
using Restaurants.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Restaurants.Infrastructure.Queries
{
    internal sealed class GetNearbyTopRatedRestaurantsQueryHandler(
        RestaurantDbContext restaurantDbContext) : IRequestHandler<GetNearbyTopRatedRestaurantsQuery, IReadOnlyCollection<NearbyRestaurant>>
    {
        private const double RadiusInMeters = 6000;

        public async Task<IReadOnlyCollection<NearbyRestaurant>> Handle(GetNearbyTopRatedRestaurantsQuery query, CancellationToken ct)
        {

            var result = await restaurantDbContext.Database
                .SqlQueryRaw<NearbyRestaurant>(
                    """
                    SELECT
                        b."Id"          AS "BranchId",
                        r."Id"          AS "RestaurantId",
                        r."BrandName"   AS "RestaurantName",
                        r."Logo"        AS "RestaurantLogoUrl",
                        4.4::real       AS "Rating",
                        r."Banner"      AS "RestaurantBanner"
                    FROM restaurants."RestaurantBranches" b
                    JOIN restaurants."Restaurants" r ON b."RestaurantId" = r."Id"
                    WHERE 
                        r."Status" = 'Active'
                        AND b."IsActive" = true
                        AND ST_DWithin(
                        ST_MakePoint(b."Longitude", b."Latitude")::geography,
                        ST_MakePoint({0}, {1})::geography,
                        {2}
                    )
                    """,
                    query.Longitude,
                    query.Latitude,
                    RadiusInMeters
                )
                .ToListAsync(ct);

            return result.AsReadOnly();
        }
    }
}
