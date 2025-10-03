using Customers.Domain.Entities;

namespace Customers.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id);
        Task AddAsync(Customer customer);
        Task<IEnumerable<Customer>> ListAsync();
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}
