using Subscriptions.Domain.Entities;

namespace Subscriptions.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<Subscription?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<Subscription>> GetAllActiveAsync(CancellationToken ct = default);
        void Add(Subscription subscription);
        void Delete(Subscription subscription);
    }
}
