using Restaurants.Domain.Entities;

namespace Restaurants.Application.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(Restaurant restaurant);
        void Update(Restaurant restaurant);
        void Delete(Restaurant restaurant);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

    }
}
