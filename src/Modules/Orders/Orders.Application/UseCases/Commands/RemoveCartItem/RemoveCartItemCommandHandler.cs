using MediatR;
using Orders.Application.Interfaces;
using Orders.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Orders.Application.UseCases.Commands.RemoveCartItem
{
    internal sealed class RemoveCartItemCommandHandler(
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<RemoveCartItemCommand>
    {
        public async Task Handle(RemoveCartItemCommand command, CancellationToken cancellationToken)
        {
            var cart = await cartRepository.GetCartByCustomerIdAsync(command.CustomerId, cancellationToken)
                ?? throw new NotFoundException("Cart", command.CustomerId);

            cart.RemoveCartItem(command.MealId, command.SizeId);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}