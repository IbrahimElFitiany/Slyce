using Food.Application.Interfaces;
using Food.Infrastructure.Persistence;

namespace Food.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly FoodDbContext _dbContext;
        
        public UnitOfWork(FoodDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
