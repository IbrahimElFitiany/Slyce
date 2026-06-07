using MediatR;

namespace Orders.Application.UseCases.Commands.UpdateOrderStatus
{
    public sealed record UpdateOrderStatusCommand(Guid OrderId, string Status) : IRequest;
}