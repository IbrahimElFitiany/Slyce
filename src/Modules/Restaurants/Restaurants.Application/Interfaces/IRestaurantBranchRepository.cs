using Restaurants.Domain.Entities;

namespace Restaurants.Application.Interfaces
{
    public interface IRestaurantBranchRepository
    {
        Task CreateAsync(RestaurantBranch branch, CancellationToken ct = default);
        Task<RestaurantBranch?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<RestaurantBranch>> GetBranchesByRestaurantIdAsync(Guid restaurantId, CancellationToken ct = default);
    }
}