using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantBranchRepository
    {
        void Add(RestaurantBranch branch);
        Task<RestaurantBranch?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}