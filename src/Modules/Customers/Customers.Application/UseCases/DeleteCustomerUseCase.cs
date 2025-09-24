using Customers.Domain.Entites;
using Customers.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
