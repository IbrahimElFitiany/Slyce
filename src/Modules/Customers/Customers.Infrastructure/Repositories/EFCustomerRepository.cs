using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Infrastructure.Persistence;

namespace Customers.Infrastructure.Repositories
{
    public sealed class EFCustomerRepository(CustomersDbContext dbContext) : ICustomerRepository
    {
        public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await dbContext.Customers.FindAsync(id, ct);
        }

        public void Add(Customer customer)
        {
            dbContext.Customers.Add(customer);
        }

        public void Delete(Customer customer)
        {
            dbContext.Customers.Remove(customer);
        }
    }
}
