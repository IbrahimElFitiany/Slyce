using Microsoft.EntityFrameworkCore;
using Orders.Application.Interfaces;
using Orders.Domain.Aggregates.Carts;
using Orders.Infrastructure.Persistence;

namespace Orders.Infrastructure.Repositores
{
    public sealed class EFCartRepository(OrdersDbContext ordersDbContext) : ICartRepository
    {
        private readonly OrdersDbContext _dbContext = ordersDbContext;

        public void Add(Cart cart)
        {
            _dbContext.Add(cart);
        }

        public async Task<Cart?> GetCartByCustomerIdAsync(Guid CustomerId, CancellationToken ct)
        {
            return await _dbContext.Carts.FirstOrDefaultAsync(c => c.CustomerId == CustomerId, ct);
        }
    }
}
