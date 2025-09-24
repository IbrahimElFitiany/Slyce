using Customers.Domain.Entites;
using Customers.Domain.Interfaces;

namespace Customers.Application.UseCases
{
    public class RegisterCustomerUseCase
    {
        private readonly ICustomerRepository _repository;
        public RegisterCustomerUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Customer> Execute(string id,string fname, string lname, string email)
        {

            var customer = new Customer(Guid.Parse(id),fname,lname,Gender.M,email,DateOnly.Parse("2004-11-25"));

            await _repository.AddAsync(customer);

            return customer;
        }

    }
}
