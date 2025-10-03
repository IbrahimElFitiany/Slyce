using Customers.Application.Interfaces;

namespace Customers.Application.UseCases
{
    public class DeleteCustomerUseCase
    {
        private readonly ICustomerRepository _repository;
        public DeleteCustomerUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(string id)
        {
            await _repository.DeleteAsync(Guid.Parse(id));
        }
    }
}
