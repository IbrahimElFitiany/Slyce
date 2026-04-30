using Restaurants.Application.Interfaces;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantDbContext _dbContext;
        
        public UnitOfWork(RestaurantDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
