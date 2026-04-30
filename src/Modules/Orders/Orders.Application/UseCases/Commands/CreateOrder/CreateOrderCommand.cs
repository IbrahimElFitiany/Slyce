using MediatR;

namespace Orders.Application.UseCases.Commands.CreateOrder
{
    public sealed record CreateOrderCommand(
        Guid CustomerId,
        Guid BranchId,
        IEnumerable<OrderItemRequest> OrderItems) : IRequest<Guid>;

    public sealed record OrderItemRequest();
}