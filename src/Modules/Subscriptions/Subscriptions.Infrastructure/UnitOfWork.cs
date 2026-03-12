using Subscriptions.Application.Interfaces;
using Subscriptions.Infrastructure.Persistence;

namespace Subscriptions.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SubscriptionsDbContext _dbContext;
        public UnitOfWork (SubscriptionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
