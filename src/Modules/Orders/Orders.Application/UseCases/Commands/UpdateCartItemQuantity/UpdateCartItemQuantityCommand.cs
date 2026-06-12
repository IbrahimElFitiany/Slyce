using MediatR;

namespace Orders.Application.UseCases.Commands.UpdateCartItemQuantity;

public sealed record UpdateCartItemQuantityCommand(
    Guid CustomerId,
    Guid MealId,
    Guid SizeId,
    int Quantity) : IRequest;