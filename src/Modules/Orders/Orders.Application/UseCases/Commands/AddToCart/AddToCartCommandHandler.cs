using MediatR;
using Menus.Contracts.Interfaces;
using Orders.Application.Interfaces;
using Orders.Domain.Aggregates.Carts;
using Orders.Domain.Repositories;
using Restaurants.Contracts.Interfaces;
using Shared.Application.Exceptions;
using Shared.Domain.Exceptions;

namespace Orders.Application.UseCases.Commands.AddToCart
{
    internal sealed class AddToCartCommandHandler(
        IMenuQueryServices menuQueryServices,
        IRestaurantQueryServices restaurantQueryServices,
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<AddToCartCommand>
    {

        public async Task Handle(AddToCartCommand command, CancellationToken ct)
        {
            var branchs = await restaurantQueryServices.GetBranchInfosAsync([command.BranchId], ct);

            var branch = branchs.GetValueOrDefault(command.BranchId)
                ?? throw new NotFoundException("Branch", command.BranchId);

            var meal = await menuQueryServices.GetMealSummaryAsync(command.MealId, ct)
                ?? throw new NotFoundException("MenuMeal", command.MealId);

            if (meal.RestaurantId != branch.RestaurantId)
                throw new InvalidDomainOperationException($"Meal '{meal.MealId}' does not belong to the restaurant");

            if (!meal.MealSizeIds.Contains(command.SizeId))
                throw new NotFoundException("MealSize", command.SizeId);


            var cart = await cartRepository.GetCartByCustomerIdAsync(command.CustomerId, ct);

            if (cart is null)
            {
                cart = new Cart(command.CustomerId, branch.BranchId);
                cartRepository.Add(cart);
            }

            cart.AddCartItem(command.MealId, command.SizeId, command.Quantity, branch.BranchId);

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}