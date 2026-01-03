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

        public async Task AddAsync(Customer customer , CancellationToken ct)
        {
            await _db.Customers.AddAsync(customer, ct);
            await _db.SaveChangesAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _db.Customers.FindAsync(id,ct);
        }
        public async Task<IEnumerable<Customer>> ListAsync(CancellationToken ct)
        {
            return await _db.Customers.ToListAsync(ct);
        }

        public async Task DeleteAsync(Guid id,CancellationToken ct)
        {
            var customer = await _db.Customers.FindAsync(id,ct);
            if (customer != null)    
            {
                _db.Customers.Remove(customer);
                await _db.SaveChangesAsync(ct);
            }
        }
        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
