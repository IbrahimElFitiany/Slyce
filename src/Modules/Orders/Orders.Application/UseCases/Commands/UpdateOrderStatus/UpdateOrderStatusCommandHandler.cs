using MediatR;
using Orders.Application.Interfaces;
using Orders.Domain.Enums;
using Shared.Application.Exceptions;

namespace Orders.Application.UseCases.Commands.UpdateOrderStatus
{
    internal sealed class UpdateOrderStatusCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateOrderStatusCommand>
    {
        //No AuthR yet
        public async Task Handle(UpdateOrderStatusCommand command, CancellationToken ct)
        {
            var order = await orderRepository.GetByIdAsync(command.OrderId, ct)
                ?? throw new NotFoundException("order");

            order.UpdateStatus(Enum.Parse<OrderStatus>(command.Status));

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}