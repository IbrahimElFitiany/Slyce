using MediatR;
using Orders.Application.Interfaces;
using Shared.Application.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Orders.Application.UseCases.Commands.UpdateCartItemQuantity;

internal sealed class UpdateCartItemQuantityCommandHandler(
    ICartRepository cartRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateCartItemQuantityCommand>
{
    public async Task Handle(UpdateCartItemQuantityCommand command, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetCartByCustomerIdAsync(command.CustomerId, cancellationToken)
            ?? throw new NotFoundException("Cart Not found");
        
        cart.UpdateCartItemQuantity(command.MealId, command.SizeId, command.Quantity);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}