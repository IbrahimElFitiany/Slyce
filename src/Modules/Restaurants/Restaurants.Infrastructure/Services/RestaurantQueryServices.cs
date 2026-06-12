using Microsoft.EntityFrameworkCore;
using Restaurants.Contracts.DTOs;
using Restaurants.Contracts.Interfaces;
using Restaurants.Infrastructure.Persistence;
using System.Drawing;

namespace Restaurants.Infrastructure.Services
{
    internal sealed class RestaurantQueryServices(RestaurantDbContext restaurantDbContext) : IRestaurantQueryServices
    {

        public async Task<bool> ExistsAsync(Guid restaurantId, CancellationToken ct)
        {
            return await restaurantDbContext.Restaurants.AnyAsync(r => r.Id == restaurantId, ct);
        }

        public async Task<BranchForSubscription?> GetBranchForSubscriptionAsync(Guid branchId, CancellationToken ct)
        {
            var branch = await restaurantDbContext.RestaurantBranches.FindAsync(branchId, ct);

            if (branch is null)
                return null;

            return new BranchForSubscription(
                RestaurantId: branch.RestaurantId,
                Longitude: branch.Address.Coordinates.Longitude,
                Latitude: branch.Address.Coordinates.Latitude,
                Schedule: branch.Schedule.ToDictionary(
                    s => s.Day,
                    s => new TimeFrame(s.OperatingHours.OpeningTime, s.OperatingHours.ClosingTime)));
        }

        public async Task<IReadOnlyDictionary<Guid, BranchInfoDTO>> GetBranchInfosAsync(IEnumerable<Guid> branchIds, CancellationToken ct)
        {
            var ids = branchIds.ToList();

            return await restaurantDbContext.RestaurantBranches
                .Where(b => ids.Contains(b.Id))
                .Join(restaurantDbContext.Restaurants,
                    b => b.RestaurantId,
                    r => r.Id,
                    (b, r) => new BranchInfoDTO(b.Id,r.Id, r.BrandName, r.Logo))
                .ToDictionaryAsync(b => b.BranchId, ct);
        }

        public async Task<IReadOnlyList<BranchInfoDTO>> GetNearByBranchesAsync(double latitude, double longitude, CancellationToken cancellationToken = default)
        {
            const double RadiusInMeters = 6000;

            var nearbyBranches = await restaurantDbContext.Database
                .SqlQueryRaw<BranchInfoDTO>(
                    """
                        SELECT
                            b."Id"              AS "BranchId",
                            b."RestaurantId",
                            r."BrandName"       AS "RestaurantName",
                            r."Logo" as "LogoUrl"
                        FROM restaurants."RestaurantBranches" b
                        INNER JOIN restaurants."Restaurants" r ON b."RestaurantId" = r."Id"
                        WHERE ST_DWithin(
                            ST_MakePoint(b."Longitude", b."Latitude")::geography,
                            ST_MakePoint({0}, {1})::geography,
                            {2}
                        )
                        ORDER BY ST_Distance(
                            ST_MakePoint(b."Longitude", b."Latitude")::geography,
                            ST_MakePoint({0}, {1})::geography
                        )
                    """,
                    longitude,
                    latitude,
                    RadiusInMeters
                )
                .ToListAsync(cancellationToken);

            return nearbyBranches.AsReadOnly();
        }

        public async Task<IReadOnlyDictionary<Guid, RestaurantBrandingDTO>> GetRestaurantBrandingByIdsAsync(IEnumerable<Guid> restaurantIds, CancellationToken cancellationToken)
        {
            return await restaurantDbContext.Restaurants
                    .Where(r => restaurantIds.Contains(r.Id))
                    .ToDictionaryAsync(
                        r => r.Id,
                        r => new RestaurantBrandingDTO(r.BrandName, r.Logo),
                        cancellationToken);
        }

        public async Task<Guid?> GetRestaurantIdByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken)
        {
            return await restaurantDbContext.Restaurants
                .Where(r => r.OwnerId == ownerId)
                .Select(r => r.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
