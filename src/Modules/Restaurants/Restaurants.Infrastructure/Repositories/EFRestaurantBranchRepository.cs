using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    public sealed class EFRestaurantBranchRepository(RestaurantDbContext restaurantDbContext): IRestaurantBranchRepository
    {

        public void Add(RestaurantBranch branch) => restaurantDbContext.RestaurantBranches.Add(branch);

        public async Task<RestaurantBranch?> GetByIdAsync(Guid Id, CancellationToken ct) => await restaurantDbContext.RestaurantBranches.FindAsync(Id, ct);
    }
}