using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    public sealed class EFRestaurantRepository(RestaurantDbContext restaurantDbContext) : IRestaurantRepository
    {
        public async Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await restaurantDbContext.Restaurants.FindAsync(id, ct);
        }

        public void Add(Restaurant restaurant) => restaurantDbContext.Restaurants.Add(restaurant);

        public void Delete(Restaurant restaurant) => restaurantDbContext.Restaurants.Remove(restaurant);
    }
}
