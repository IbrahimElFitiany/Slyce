using Orders.Application.Interfaces;
using Orders.Infrastructure.Persistence;

namespace Orders.Infrastructure
{
    public sealed class UnitOfWork(OrdersDbContext ordersDbContext) : IUnitOfWork
    {
        private readonly OrdersDbContext _ordersDbContext = ordersDbContext;

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
           await _ordersDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}