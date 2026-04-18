using MediatR;
using Menus.Application.UseCases.Queries.GetMealByID;
using Menus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Application.Exceptions;
using System.Text.Json;

namespace Menus.Infrastructure.Queries
{
    internal sealed class GetMealByIdQueryHandler : IRequestHandler<GetMealByIdQuery, GetMealByIdQueryResponse>
    {
        private readonly MenusDbContext _menusDbContext;
        private readonly IDistributedCache _distributedCache;

        public GetMealByIdQueryHandler(MenusDbContext dbContext, IDistributedCache distributedCache)
        {
            _menusDbContext = dbContext;
            _distributedCache = distributedCache;
        }

        public async Task<GetMealByIdQueryResponse> Handle(GetMealByIdQuery query, CancellationToken ct)
        {
            var key = $"meal:{query.MealId}";

            var cachedMeal = await _distributedCache.GetStringAsync(key, ct);

            if (string.IsNullOrEmpty(cachedMeal))
            {
                var meal = await _menusDbContext.MenuMeals
                .Where(m => m.Id == query.MealId && m.Reviewed == true)
                .Select(m => new GetMealByIdQueryResponse(
                    m.Id,
                    m.Name,
                    m.Description,
                    m.Image,
                    m.Available,
                    m.Sizes
                        .OrderBy(s => s.SortOrder)
                        .Select(s => new MealSizeResponse(
                            s.Id,
                            s.Name,
                            s.Price.Amount,
                            s.Price.Currency,
                            s.SortOrder,
                            s.IngredientQuantities
                                .Join(
                                    m.Ingredients,
                                    iq => iq.MealIngredientId,
                                    i => i.FoodId,
                                    (iq, i) => new MealIngredientResponse(i.Name, iq.Quantity)
                                )
                                .ToList(),
                            new NutritionResponse(
                                s.Nutrition.Calories,
                                s.Nutrition.Protein,
                                s.Nutrition.TotalFat,
                                s.Nutrition.SaturatedFat,
                                s.Nutrition.TransFat,
                                s.Nutrition.Cholesterol,
                                s.Nutrition.SodiumMg,
                                s.Nutrition.TotalCarbohydrate,
                                s.Nutrition.DietaryFiber,
                                s.Nutrition.SugarGrams,
                                s.Nutrition.VitaminD,
                                s.Nutrition.CalciumMg,
                                s.Nutrition.IronMg,
                                s.Nutrition.PotassiumMg,
                                s.Nutrition.VitaminAMcg,
                                s.Nutrition.VitaminCMg)
                        )).ToList()
                ))
                .FirstOrDefaultAsync(ct) ?? throw new NotFoundException("Meal", query.MealId);

                await _distributedCache.SetStringAsync(
                    key: key,
                    value: JsonSerializer.Serialize(meal),
                    options: new DistributedCacheEntryOptions{AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2)},
                    ct);

                return meal;   
            }


            var result = JsonSerializer.Deserialize<GetMealByIdQueryResponse>(cachedMeal);
            return result ?? throw new Exception("Cache deserialization failed");
        }
    }
}