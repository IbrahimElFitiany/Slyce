using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Infrastructure.Persistence;

namespace Customers.Infrastructure.Repositories
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private readonly CustomersDbContext _dbContext;

        public EFCustomerRepository (CustomersDbContext customersDbContext)
        {
            _dbContext = customersDbContext;
        }

        public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _dbContext.Customers.FindAsync(id, ct);
        }

        public void Add(Customer customer)
        {
            _dbContext.Customers.Add(customer);
        }

        public void Delete(Customer customer)
        {
            _dbContext.Customers.Remove(customer);
        }
    }
}
