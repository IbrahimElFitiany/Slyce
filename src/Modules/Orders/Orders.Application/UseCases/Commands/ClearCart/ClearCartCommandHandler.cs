using MediatR;
using Orders.Application.Interfaces;
using Shared.Application.Exceptions;

namespace Orders.Application.UseCases.Commands.ClearCart
{
    internal sealed class ClearCartCommandHandler(
        IUnitOfWork unitOfWork,
        ICartRepository cartRepository) : IRequestHandler<ClearCartCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICartRepository _cartRepository = cartRepository;

        public async Task Handle(ClearCartCommand command, CancellationToken ct)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(command.CustomerId, ct)
                ?? throw new NotFoundException("cart not Found");

            _cartRepository.Remove(cart);

            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
