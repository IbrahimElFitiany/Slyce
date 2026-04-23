using MediatR;
using Orders.Application.Interfaces;
using Orders.Domain.Enums;
using Shared.Application.Exceptions;

namespace Orders.Application.UseCases.Commands.UpdateOrderStatus
{
    internal sealed class UpdateOrderStatusCommandHandler(IOrderRepository orderRepository, IOrderNotifier orderNotifier) : IRequestHandler<UpdateOrderStatusCommand>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IOrderNotifier _notifier = orderNotifier;

        public async Task Handle(UpdateOrderStatusCommand command, CancellationToken ct)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId)
                ?? throw new NotFoundException("order");

            OrderStatus orderStatus;
            Enum.TryParse(command.Status, out orderStatus);

            order.UpdateStatus(orderStatus);

            await _orderRepository.UpdateStatusAsync(order);

            await _notifier.NotifyOrderStatusChanged(order.Id, Guid.Parse("e4ad1378-d90a-4d28-97cc-535adb69419a"), orderStatus);
        }
    }
}
