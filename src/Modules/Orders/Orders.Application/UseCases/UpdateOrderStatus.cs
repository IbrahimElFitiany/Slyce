
using Orders.Application.Interfaces;
using Orders.Domain.Enums;

public class UpdateOrderStatus
{
    private readonly IOrderRepository _repository;
    private readonly IOrderNotifier _notifier;

    public UpdateOrderStatus(IOrderRepository repository, IOrderNotifier notifier)
    {
        _repository = repository;
        _notifier = notifier;
    }

    public async Task Execute(Guid orderId, OrderStatus status)
    {
        var order = await _repository.GetByIdAsync(orderId);
        if (order is null) throw new Exception("Order not found");

        order.UpdateStatus(status);

        await _repository.UpdateStatusAsync(order);

        await _notifier.NotifyOrderStatusChanged(orderId,Guid.Parse("e4ad1378-d90a-4d28-97cc-535adb69419a"), status);
    }
}
