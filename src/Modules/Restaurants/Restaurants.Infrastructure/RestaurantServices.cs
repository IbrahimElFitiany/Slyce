using Microsoft.EntityFrameworkCore;
using Restaurants.Contracts;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly RestaurantDbContext _db;
        public RestaurantServices (RestaurantDbContext restaurantDbContext) {
            _db = restaurantDbContext;
        }

        public async Task<bool> ExistsAsync(Guid restaurantId)
        {
            return await _db.Restaurants.AnyAsync(r => r.Id == restaurantId);
        }
    }
}
