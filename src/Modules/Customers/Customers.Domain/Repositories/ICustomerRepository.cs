using Customers.Domain.Entities;

namespace Customers.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(Customer customer);
        void Delete(Customer customer);
    }
}
