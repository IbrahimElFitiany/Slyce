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

        public async Task Handle(AddToCartCommand command, CancellationToken ct)
        {
            var meal = await menuQueryServices.GetMealSummaryAsync(command.MealId, ct) 
                ?? throw new NotFoundException("MenuMeal", command.MealId);

            if (!meal.MealSizeIds.Contains(command.SizeId))
                throw new NotFoundException("MealSize", command.SizeId);

            var cart = await cartRepository.GetCartByCustomerIdAsync(command.CustomerId, ct);

            if (cart is null)
            {
                cart = new Cart(command.CustomerId, meal.RestaurantId);
                cartRepository.Add(cart);
            }

            cart.AddCartItem(command.MealId, command.SizeId, command.Quantity, meal.RestaurantId);

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}