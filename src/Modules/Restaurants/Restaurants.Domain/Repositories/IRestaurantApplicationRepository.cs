using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantApplicationRepository
    {
        void Add(RestaurantApplication restaurantApplication);
        Task<RestaurantApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> ExistsByBrandNameAsync(string brandName, CancellationToken cancellationToken = default);
        Task<bool> ExistsByMobileNumberAsync(string mobileNumber, CancellationToken cancellationToken = default);

    }
}
