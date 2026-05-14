using Menus.Contracts.DTOs;
using Menus.Contracts.Interfaces;
using Menus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Menus.Infrastructure.Services
{
    internal sealed class MenuQueryServices(MenusDbContext context) : IMenuQueryServices
    {
        public async Task<IReadOnlyCollection<MealSizeDTO>> GetMealSizesByRestaurantAsync(IEnumerable<Guid> sizeIds, Guid restaurantId, CancellationToken cancellationToken)
        {
            var sizesIdList = sizeIds.Distinct().ToList();

            var sizes = await context.MenuMeals
                .Where(mm => mm.RestaurantId == restaurantId)
                .SelectMany(mm => mm.Sizes
                    .Where(ms => sizesIdList.Contains(ms.Id))
                    .Select(ms => new MealSizeDTO(
                        mm.Id,
                        mm.Name,
                        ms.Id,
                        ms.Name,
                        ms.Price.Amount,
                        ms.Price.Currency
                    )))
                .ToListAsync(cancellationToken);

            return sizes;
        }

        public async Task<MealSummaryDTO?> GetMealSummaryAsync(Guid mealId, CancellationToken cancellationToken)
        {
            var mealSummary = await context.MenuMeals
                .Where(m => m.Id == mealId)
                .Select(m => new MealSummaryDTO(
                    m.RestaurantId,
                    m.Id,
                    m.Sizes.Select(s => s.Id).ToList()
                ))
                .FirstOrDefaultAsync(cancellationToken);

            return mealSummary;
        }

        public async Task<IReadOnlyDictionary<(Guid MealId, Guid SizeId), MealSizeInfoDTO>> GetMealSizeSnapshotsAsync(
            IEnumerable<(Guid MealId, Guid SizeId)> keys,
            CancellationToken cancellationToken)
        {
            var keyList = keys.ToList();
            var mealIds = keyList.Select(k => k.MealId).ToArray();
            var sizeIds = keyList.Select(k => k.SizeId).ToArray();

            var rows = await context.Database
                .SqlQuery<MealSizeInfoDTO>(
                    $"""
                     SELECT
                         mm."Id"                       AS MealId,
                         ms."Id"                       AS SizeId,
                         mm."Name"                     AS MealName,
                         ms."Name"                     AS SizeName,
                         mm."Description"              AS Description,
                         mm."Image"                 AS ImageUri,
                         ms.price_amount               AS Price,
                         NULL::numeric AS DiscountedPrice,
                         ms.price_currency             AS Currency,
                         ms."Calories"         AS Calories
                     FROM menus."MenuMeals" mm
                     JOIN menus."MealSizes" ms ON ms."MealId" = mm."Id"
                     JOIN unnest({mealIds}, {sizeIds}) AS pairs(meal_id, size_id)
                       ON mm."Id" = pairs.meal_id AND ms."Id" = pairs.size_id
                     """)
                .ToListAsync(cancellationToken);

            return rows.ToDictionary(r => (r.MealId, r.SizeId));
        }
    }
}