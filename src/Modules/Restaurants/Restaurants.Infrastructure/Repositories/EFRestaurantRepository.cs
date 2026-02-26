using Microsoft.EntityFrameworkCore;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    public sealed class EFRestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public EFRestaurantRepository(RestaurantDbContext restaurantDbContext)
        {
            _dbContext = restaurantDbContext;
        }

        public async Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Restaurants
                .FindAsync(id,cancellationToken);
        }

        public void Add(Restaurant restaurant)
        {
            _dbContext.Restaurants
                .Add(restaurant);
        }

        public void Update(Restaurant restaurant)
        {
            _dbContext.Restaurants
                .Update(restaurant);
        }

        public void Delete(Restaurant restaurant)
        {
            _dbContext.Restaurants
                .Remove(restaurant);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Restaurants
                .AnyAsync(r => r.BrandName == name, cancellationToken);
        }
    }
}
