using Orders.Application.Interfaces;
using Orders.Domain.Aggregates.Order;
using Orders.Infrastructure.Persistence;

namespace Orders.Infrastructure.Repositores
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly OrdersDbContext _context;

        public EFOrderRepository(OrdersDbContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }
        public void UpdateStatus(Order order)
        {
            _context.Orders.Update(order);
        }
        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

    }
}
