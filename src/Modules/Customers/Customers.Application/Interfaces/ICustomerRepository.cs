using Customers.Domain.Entities;

namespace Customers.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(Customer customer);
        void Delete(Customer customer);
    }
}
