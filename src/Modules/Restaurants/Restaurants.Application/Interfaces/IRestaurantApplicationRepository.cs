using Restaurants.Domain.Entities;

namespace Restaurants.Application.Interfaces
{
    public interface IRestaurantApplicationRepository
    {
        Task<RestaurantApplication> AddAsync(RestaurantApplication restaurantApplication, CancellationToken cancellationToken = default);
        Task<RestaurantApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<RestaurantApplication>> ListAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> ExistsByMobileNumberAsync(string mobileNumber);

    }
}
