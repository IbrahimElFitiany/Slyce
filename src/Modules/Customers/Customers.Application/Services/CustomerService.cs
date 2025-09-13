using Customers.Application.Interfaces;
using Customers.Domain.Entites;
using Customers.Domain.Interfaces;

namespace Customers.Application.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        //--------------------------------

        public async Task AddCustomer(Customer customer)
        {
            await _repository.AddAsync(customer);
        }

        public async Task<IEnumerable<Customer>> ListCustomers()
        {
           return await _repository.ListAsync();
        }

        public async Task UpdateEmailAsync(Guid customerId, string newEmail)
        {
            var customer = await _repository.GetByIdAsync(customerId);
            if (customer == null) throw new Exception("NOT FOUND");
            customer.UpdateEmail(newEmail);
            await _repository.SaveChangesAsync();
        }
    }
}
