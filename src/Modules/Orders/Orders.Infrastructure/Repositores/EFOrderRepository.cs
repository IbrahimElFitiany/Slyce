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

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task UpdateStatusAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
