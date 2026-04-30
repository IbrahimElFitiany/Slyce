using MediatR;
using Menus.Contracts.Interfaces;
using Orders.Application.Interfaces;
using Shared.Application.Exceptions;

namespace Orders.Application.UseCases.Queries.ViewCart
{
    internal sealed class ViewCartQueryHandler(
        ICartRepository cartRepository,
        IMenuQueryServices menuQueryServices) : IRequestHandler<ViewCartQuery, ViewCartQueryResult>
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IMenuQueryServices _menuQueryServices = menuQueryServices;
        
        public async Task<ViewCartQueryResult> Handle(ViewCartQuery query, CancellationToken ct)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(query.CustomerId, ct);

            if (cart is null || cart.CartItems.Count == 0)
                return new ViewCartQueryResult([]);

            var keys = cart.CartItems.Select(i => (i.MealId, i.SizeId));

            var mealSizeInfo = await _menuQueryServices.GetMealSizeSnapshotsAsync(keys, ct);

            var cartItems = cart.CartItems.Select(item =>
            {
                var info = mealSizeInfo[(item.MealId, item.SizeId)];

                return new CartItemResult(
                    item.MealId,
                    info.MealName,
                    info.ImageUri,
                    info.Description,
                    item.SizeId,
                    info.SizeName,
                    item.Quantity,
                    info.Price,
                    info.Currency,
                    info.Calories);
            }).ToList();

            return new ViewCartQueryResult(cartItems);
        }
    }
}
