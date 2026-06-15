using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantRepository
    {
        Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(Restaurant restaurant);
        void Delete(Restaurant restaurant);
    }
}