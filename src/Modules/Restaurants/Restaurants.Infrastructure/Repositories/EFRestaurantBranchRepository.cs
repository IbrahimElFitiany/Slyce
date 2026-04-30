using Microsoft.EntityFrameworkCore;
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

        public void Add(RestaurantBranch branch)
        {
            _dbContext.RestaurantBranches.Add(branch);
        }

        public async Task<RestaurantBranch?> GetByIdAsync(Guid Id, CancellationToken ct = default)
        {
            return await _dbContext.RestaurantBranches
                .FindAsync(Id, ct);
        }
    }
}
