using Food.Contracts;
using Food.Contracts.DTOs;
using Food.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Food.Infrastructure.Services
{
    internal sealed class FoodQueryServices(FoodDbContext dbContext) : IFoodQueryServices
    {
        public async Task<bool> AllAllergensExistAsync(IEnumerable<Guid> allergenIds, CancellationToken cancellationToken)
        {
            var ids = allergenIds.Distinct().ToList();
            var count = await dbContext.Allergens.CountAsync(a => ids.Contains(a.Id), cancellationToken);

            return count == ids.Count;
        }

        public async Task<bool> AllFoodPreferencesExistAsync(IEnumerable<Guid> foodPreferenceIds, CancellationToken cancellationToken)
        {
            var ids = foodPreferenceIds.Distinct().ToList();
            var count = await dbContext.FoodPreferences.CountAsync(fp => ids.Contains(fp.Id), cancellationToken);

            return count == ids.Count;
        }

        public async Task<Dictionary<Guid, FoodNutritionDTO>> GetFoodNutritionsAsync(IEnumerable<Guid> foodIds, CancellationToken cancellationToken)
        {
            var ids = foodIds.ToList();
            return await dbContext.Foods
                .Where(f => ids.Contains(f.Id))
                .Select(f => new FoodNutritionDTO(
                    f.Id,
                    f.Name,
                    f.NutritionPer100g.CalciumMg,
                    f.NutritionPer100g.Calories,
                    f.NutritionPer100g.Cholesterol,
                    f.NutritionPer100g.DietaryFiber,
                    f.NutritionPer100g.IronMg,
                    f.NutritionPer100g.PotassiumMg,
                    f.NutritionPer100g.Protein,
                    f.NutritionPer100g.SaturatedFat,
                    f.NutritionPer100g.SodiumMg,
                    f.NutritionPer100g.SugarGrams,
                    f.NutritionPer100g.TotalCarbohydrate,
                    f.NutritionPer100g.TotalFat,
                    f.NutritionPer100g.TransFat,
                    f.NutritionPer100g.VitaminAMcg,
                    f.NutritionPer100g.VitaminCMg,
                    f.NutritionPer100g.VitaminD
                )).ToDictionaryAsync(f => f.Id, cancellationToken);
        }
    }
}