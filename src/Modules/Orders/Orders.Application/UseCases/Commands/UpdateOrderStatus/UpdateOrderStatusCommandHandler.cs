using MediatR;
using Orders.Application.Interfaces;
using Orders.Domain.Enums;
using Shared.Application.Exceptions;

namespace Orders.Application.UseCases.Commands.UpdateOrderStatus
{
    internal sealed class UpdateOrderStatusCommandHandler(IOrderRepository orderRepository, IOrderNotifier orderNotifier) : IRequestHandler<UpdateOrderStatusCommand>
    {

        public async Task Handle(UpdateOrderStatusCommand command, CancellationToken ct)
        {
            var order = await orderRepository.GetByIdAsync(command.OrderId, ct)
                ?? throw new NotFoundException("order");

            OrderStatus orderStatus;
            Enum.TryParse(command.Status, out orderStatus);

            order.UpdateStatus(orderStatus);

            orderRepository.UpdateStatus(order);

            await orderNotifier.NotifyOrderStatusChanged(order.Id, Guid.Parse("e4ad1378-d90a-4d28-97cc-535adb69419a"), orderStatus);
        }
    }
}
