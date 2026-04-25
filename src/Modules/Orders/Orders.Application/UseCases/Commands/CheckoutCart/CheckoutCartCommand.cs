using MediatR;

namespace Orders.Application.UseCases.Commands.CheckoutCart
{
    public sealed record CheckoutCartCommand(Guid CustomerId) : IRequest;
}
