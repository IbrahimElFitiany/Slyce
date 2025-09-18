
using Orders.Application.Interfaces;
using Orders.Domain.Entities;
using Orders.Domain.Enums;

public class CreateOrder
{
    private readonly IOrderRepository _repository;

    public CreateOrder(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Execute(Guid customerId)
    {
        var order = new Order(customerId, Guid.NewGuid(), 1500,PaymentMethod.Cash, Guid.NewGuid());
        await _repository.AddAsync(order);
        return order.Id;
    }
}
