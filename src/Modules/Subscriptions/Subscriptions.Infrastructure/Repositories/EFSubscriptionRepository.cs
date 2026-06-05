using Microsoft.EntityFrameworkCore;
using Subscriptions.Application.Interfaces;
using Subscriptions.Domain.Entities;
using Subscriptions.Domain.Enums;
using Subscriptions.Infrastructure.Persistence;

namespace Subscriptions.Infrastructure.Repositories
{
    internal sealed class EFSubscriptionRepository(SubscriptionsDbContext dbContext) : ISubscriptionRepository
    {

        public void Add(Subscription subscription) => dbContext.Add(subscription);

        public void Delete(Subscription subscription) => dbContext.Remove(subscription);
        public async Task<Subscription?> GetByIdAsync(Guid id, CancellationToken ct = default) => await dbContext.Subscriptions.FindAsync(id, ct);

        public Task<List<Subscription>> GetAllActiveAsync(CancellationToken ct = default)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            return dbContext.Subscriptions
                .Where(
                s => s.Status == SubscriptionStatus.Active &&
                s.StartDate <= today &&
                s.EndDate >= today &&
                s.DeliveryDays.Any(d => d.Day == today.DayOfWeek)).ToListAsync(ct);
        }


    }
}