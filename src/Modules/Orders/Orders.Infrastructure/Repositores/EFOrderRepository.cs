using Orders.Application.Interfaces;
using Orders.Domain.Aggregates.Order;
using Orders.Infrastructure.Persistence;

namespace Orders.Infrastructure.Repositores
{
    internal sealed class EFOrderRepository(OrdersDbContext context) : IOrderRepository
    {
        public void Add(Order order)  => context.Orders.Add(order);
        public void AddRange(IEnumerable<Order> orders) => context.Orders.AddRange(orders);
        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct) => await context.Orders.FindAsync(id, ct);
    }
}