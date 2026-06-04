using Orders.Domain.Aggregates.Order;

namespace Orders.Application.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void AddRange(IEnumerable<Order> orders);
        void UpdateStatus(Order order);
        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
