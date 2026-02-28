using Customers.Application.Interfaces;
using Customers.Infrastructure.Persistence;

namespace Customers.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomersDbContext _dbContext;
        
        public UnitOfWork(CustomersDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
