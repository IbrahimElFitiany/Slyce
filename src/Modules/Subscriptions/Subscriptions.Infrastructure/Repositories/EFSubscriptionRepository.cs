using Microsoft.EntityFrameworkCore;
using Subscriptions.Application.Interfaces;
using Subscriptions.Domain.Entities;
using Subscriptions.Infrastructure.Persistence;

namespace Subscriptions.Infrastructure.Repositories
{
    public class EFSubscriptionRepository : ISubscriptionRepository
    {
        private readonly SubscriptionsDbContext _dbContext;

        public EFSubscriptionRepository(SubscriptionsDbContext db)
        {
            _dbContext = db;
        }

        public void Add(Subscription subscription)
        {
            _dbContext.Add(subscription);
        }
        public void Delete(Subscription subscription)
        {
            _dbContext.Remove(subscription);
        }
        public async Task<Subscription?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbContext.Subscriptions.FindAsync(id, ct);
        }
    }
}