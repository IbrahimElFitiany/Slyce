using Menus.Contracts.DTOs;
using Menus.Contracts.Interfaces;
using Menus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Menus.Infrastructure.Services
{
    internal sealed class MenuQueryServices(MenusDbContext context) : IMenuQueryServices
    {
        private readonly MenusDbContext _context = context;

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

            return sizes;
        }

        public async Task<MealSummaryDTO?> GetMealSummaryAsync(Guid mealId, CancellationToken cancellationToken)
        {
            var mealSummary = await _context.MenuMeals
                .Where(m => m.Id == mealId)
                .Select(m => new MealSummaryDTO(
                    m.RestaurantId,
                    m.Id,
                    m.Sizes.Select(s => s.Id).ToList()
                ))
                .FirstOrDefaultAsync(cancellationToken);

            return mealSummary;
        }
    }
}