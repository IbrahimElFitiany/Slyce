using Restaurants.Contracts.DTOs;

namespace Restaurants.Contracts.Interfaces
{
    public interface IRestaurantServices
    {
        Task<bool> ExistsAsync(Guid restaurantId, CancellationToken cancellationToken);
        
        Task<BranchForSubscription> GetBranchForSubscriptionAsync(Guid branchId, CancellationToken cancellationToken);

    }
}
