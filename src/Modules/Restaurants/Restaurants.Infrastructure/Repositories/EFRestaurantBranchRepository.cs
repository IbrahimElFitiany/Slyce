using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    public sealed class EFRestaurantBranchRepository : IRestaurantBranchRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public EFRestaurantBranchRepository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CreateAsync(RestaurantBranch branch, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<RestaurantBranch?>> GetBranchesByRestaurantIdAsync(Guid RestaurantId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<RestaurantBranch?> GetByIdAsync(Guid Id, CancellationToken ct = default)
        {
            return await _dbContext.RestaurantBranches.FindAsync(Id, ct);
        }
    }
}
