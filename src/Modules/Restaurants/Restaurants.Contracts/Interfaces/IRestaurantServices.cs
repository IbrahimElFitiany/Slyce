using Restaurants.Contracts.DTOs;

namespace Restaurants.Contracts.Interfaces
{
    public interface IRestaurantServices
    {
        Task<bool> ExistsAsync(Guid restaurantId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves the branch details required to create a subscription,
        /// including its schedule, location, and restaurant association.
        /// </summary>
        /// <returns>A <see cref="BranchForSubscription"/> containing the branch details, or <c>null</c> if not found.</returns>
        Task<BranchForSubscription?> GetBranchForSubscriptionAsync(Guid branchId, CancellationToken cancellationToken);
    }
}
