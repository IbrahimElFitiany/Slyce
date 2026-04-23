using MediatR;
using Menus.Contracts.Interfaces;
using Orders.Application.Interfaces;
using Orders.Domain.Aggregates.Carts;
using Shared.Application.Exceptions;

namespace Orders.Application.UseCases.Commands.AddToCart
{
    internal sealed class AddToCartCommandHandler(
        IMenuQueryServices menuQueryServices,
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<AddToCartCommand>
    {
        private readonly IMenuQueryServices _menuQueryServices = menuQueryServices;
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(AddToCartCommand command, CancellationToken ct)
        {
            var meal = await _menuQueryServices.GetMealSummaryAsync(command.MealId, ct) ??
                throw new NotFoundException("MenuMeal", command.MealId);

            if (!meal.MealSizeIds.Contains(command.SizeId))
                throw new NotFoundException("MealSize", command.SizeId);

            var cart = await _cartRepository.GetCartByCustomerIdAsync(command.CustomerId, ct);

            if (cart is null)
            {
                cart = new Cart(command.CustomerId, meal.RestaurantId);
                _cartRepository.Add(cart);
            }

            cart.AddCartItem(command.MealId, command.SizeId, command.Quantity, meal.RestaurantId);

            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}