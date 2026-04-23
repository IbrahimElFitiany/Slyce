using MediatR;

namespace Orders.Application.UseCases.Commands.AddToCart
{
    public sealed record AddToCartCommand(
        Guid CustomerId,
        Guid MealId,
        Guid SizeId,
        int Quantity) : IRequest;
}
