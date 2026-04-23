using MediatR;
using Orders.Application.Interfaces;
using Orders.Domain.Enums;

namespace Orders.Application.UseCases.Commands.UpdateOrderStatus
{
    public sealed record UpdateOrderStatusCommand(Guid OrderId, string Status) : IRequest;
}