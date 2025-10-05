using Restaurants.Domain.Entities;

namespace Restaurants.Application.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Restaurant>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
        Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
        Task DeleteAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

    }
}
