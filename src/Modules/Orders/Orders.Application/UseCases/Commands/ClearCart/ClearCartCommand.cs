using MediatR;

namespace Orders.Application.UseCases.Commands.ClearCart
{
    public sealed record ClearCartCommand(Guid CustomerId) : IRequest;
}
