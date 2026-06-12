using MediatR;

namespace Orders.Application.UseCases.Commands.RemoveCartItem
{
    public sealed record RemoveCartItemCommand(Guid CustomerId, Guid MealId, Guid SizeId) : IRequest;

}
