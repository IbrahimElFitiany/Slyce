using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure.Repositories
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private readonly CustomersDbContext _db;
        public EFCustomerRepository (CustomersDbContext db)
        {
            _db = db;
        }
        //---------------------

        public async Task AddAsync(Customer customer)
        {
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _db.Customers.FindAsync(id);
        }
        public async Task<IEnumerable<Customer>> ListAsync()
        {
            return await _db.Customers.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
