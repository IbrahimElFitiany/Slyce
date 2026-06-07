using Orders.Domain.Aggregates.Order;

namespace Orders.Application.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void AddRange(IEnumerable<Order> orders);
        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
