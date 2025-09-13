using Customers.Domain.Entites;

namespace Customers.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> ListCustomers();
        Task AddCustomer (Customer customer);
        Task UpdateEmailAsync(Guid customerId, string newEmail);

    }
}
