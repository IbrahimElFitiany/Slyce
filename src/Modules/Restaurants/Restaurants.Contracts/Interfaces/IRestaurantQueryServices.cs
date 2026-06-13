using Restaurants.Contracts.DTOs;

namespace Restaurants.Contracts.Interfaces
{
    public interface IRestaurantQueryServices
    {
        Task<bool> ExistsAsync(Guid restaurantId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves the branch details required to create a subscription,
        /// including its schedule, location, and restaurant association.
        /// </summary>
        /// <returns>A <see cref="BranchForSubscription"/> containing the branch details, or <c>null</c> if not found.</returns>
        Task<BranchForSubscription?> GetBranchForSubscriptionAsync(Guid branchId, CancellationToken cancellationToken);

        Task<Guid?> GetRestaurantIdByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken);

        Task<IReadOnlyDictionary<Guid, BranchInfoDTO>> GetBranchInfosAsync(IEnumerable<Guid> branchIds, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves the minimal branding information for a batch of restaurants.
        /// </summary>
        Task<IReadOnlyDictionary<Guid, RestaurantBrandingDTO>> GetRestaurantBrandingByIdsAsync(IEnumerable<Guid> restaurantIds, CancellationToken cancellationToken);

        Task<IReadOnlyList<BranchInfoDTO>> GetActiveNearByBranchesAsync(double latitude, double longitude, CancellationToken cancellationToken = default);
    }
}
