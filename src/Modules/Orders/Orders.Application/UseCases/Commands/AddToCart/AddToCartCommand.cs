using MediatR;

namespace Orders.Application.UseCases.Commands.AddToCart
{
    public sealed record AddToCartCommand(
        Guid CustomerId,
        Guid RestaurantId,
        Guid MealId,
        Guid SizeId,
        int Quantity) : IRequest;
}
