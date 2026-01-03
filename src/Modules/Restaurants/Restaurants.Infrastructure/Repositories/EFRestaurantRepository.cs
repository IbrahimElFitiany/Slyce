using Microsoft.EntityFrameworkCore;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    public sealed class EFRestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantDbContext _db;
        public EFRestaurantRepository(RestaurantDbContext restaurantDbContext)
        {
            _db = restaurantDbContext;
        }
        public async Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Restaurants
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Restaurants.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
        {
            await _db.Restaurants.AddAsync(restaurant);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
        {
            _db.Restaurants.Update(restaurant);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
        {
            _db.Restaurants.Remove(restaurant);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _db.Restaurants.AnyAsync(r => r.BrandName == name, cancellationToken);
        }
    }
}
