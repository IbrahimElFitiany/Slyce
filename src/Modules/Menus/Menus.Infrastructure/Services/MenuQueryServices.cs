using Menus.Contracts.DTOs;
using Menus.Contracts.Interfaces;
using Menus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Exceptions;

namespace Menus.Infrastructure.Services
{
    public sealed class MenuQueryServices : IMenuQueryServices
    {
        private readonly MenusDbContext _context;

        public MenuQueryServices(MenusDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<MealSizeDTO>> GetMealSizesAsync(IEnumerable<Guid> sizeIds, Guid restaurantId, CancellationToken cancellationToken)
        {
            var sizesIdList = sizeIds.Distinct().ToList();
            
            var sizes = await _context.Database
                .SqlQuery<MealSizeDTO>(
                    $"""
                     select
                         mm."Id" as MealId,
                         mm."Name" as MealName,
                         ms."Id" as MealSizeId,
                         ms."Name"  as SizeName,
                         ms.price_amount as PriceAmountAtSubscription,
                         ms.price_currency as PriceCurrency
                     from menus."MenuMeals" as mm
                     join menus."MealSizes" as ms
                     on ms."MealId" = mm."Id"
                     where mm."RestaurantId" = {restaurantId}
                     and ms."Id" = any ({sizesIdList})
                     """).ToListAsync(cancellationToken);

            if (sizes.Count != sizesIdList.Count)
                throw new NotFoundException ("One or more meal sizes do not exist or do not belong to the restaurant");

            return sizes;
        }
    }
}