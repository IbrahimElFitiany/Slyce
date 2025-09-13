using Customers.Domain.Entites;

namespace Customers.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id);
        Task AddAsync(Customer customer);
        Task<IEnumerable<Customer>> ListAsync();
        Task SaveChangesAsync();
    }
}
