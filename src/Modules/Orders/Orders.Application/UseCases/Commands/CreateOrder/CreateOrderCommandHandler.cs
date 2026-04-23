using Customers.Contracts.Interfaces;
using MediatR;
using Orders.Application.Interfaces;

namespace Orders.Application.UseCases.Commands.CreateOrder
{
    internal sealed class CreateOrderCommandHandler(
        IOrderRepository repository,
        ICustomerServices customerServices) : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _repository = repository;
        private readonly ICustomerServices _customerServices = customerServices;

        public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken ct)
        {
            //check for customer
            //if (!await _customerServices.CustomerExists(command.CustomerId, ct))
            //    throw new NotFoundException("customer", command.CustomerId);

            //var order = new Order(command.CustomerId, Guid.NewGuid(), 1500, PaymentMethod.Cash, Guid.NewGuid());
            //await _repository.AddAsync(order);
            return Guid.NewGuid();
        }
    }
}