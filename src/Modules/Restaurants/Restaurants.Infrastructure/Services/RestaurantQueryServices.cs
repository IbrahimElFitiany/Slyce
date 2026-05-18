using Microsoft.EntityFrameworkCore;
using Restaurants.Contracts.DTOs;
using Restaurants.Contracts.Interfaces;
using Restaurants.Infrastructure.Persistence;

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
                    (b, r) => new BranchInfoDTO(b.Id,r.Id, r.BrandName, r.Image))
                .ToDictionaryAsync(b => b.BranchId, ct);
        }
    }
}
