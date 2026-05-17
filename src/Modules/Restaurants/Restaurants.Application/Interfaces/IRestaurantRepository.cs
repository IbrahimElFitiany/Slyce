using Restaurants.Domain.Entities;

namespace Restaurants.Application.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(Restaurant restaurant);
        void Delete(Restaurant restaurant);
    }
}