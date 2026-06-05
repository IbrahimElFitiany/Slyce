using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<Subscription?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<Subscription>> GetAllActiveAsync(CancellationToken ct = default);
        void Add(Subscription subscription);
        void Delete(Subscription subscription);
    }
}
