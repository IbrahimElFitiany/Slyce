using Food.Application.Interfaces;
using Food.Contracts;
using Food.Contracts.DTOs;

namespace Food.Application.Services
{
    public class FoodContractServices : IFoodServices
    {
        private readonly IFoodRepository _foodRepository;

        public FoodContractServices(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<Dictionary<Guid, FoodNutritionDTO>> GetFoodNutritionsAsync(IEnumerable<Guid> foodIds, CancellationToken cancellationToken)
        {
            var foods = await _foodRepository.GetByIdsAsync(foodIds,cancellationToken);

            var foodDTOs = foods.Select(f =>
                 new FoodNutritionDTO(
                    Id: f.Id,
                    Name: f.Name,
                    CalciumMg: f.NutritionPer100g.CalciumMg,
                    Calories: f.NutritionPer100g.Calories,
                    Cholesterol: f.NutritionPer100g.Cholesterol,
                    DietaryFiber: f.NutritionPer100g.DietaryFiber,
                    IronMg: f.NutritionPer100g.IronMg,
                    PotassiumMg: f.NutritionPer100g.PotassiumMg,
                    Protein: f.NutritionPer100g.Protein,
                    SaturatedFat: f.NutritionPer100g.SaturatedFat,
                    SodiumMg: f.NutritionPer100g.SodiumMg,
                    SugarGrams: f.NutritionPer100g.SugarGrams,
                    TotalCarbohydrate: f.NutritionPer100g.TotalCarbohydrate,
                    TotalFat: f.NutritionPer100g.TotalFat,
                    TransFat: f.NutritionPer100g.TransFat,
                    VitaminAMcg: f.NutritionPer100g.VitaminAMcg,
                    VitaminCMg: f.NutritionPer100g.VitaminCMg,
                    VitaminD: f.NutritionPer100g.VitaminD
                    )
            ).ToList();

            return foodDTOs.ToDictionary(f => f.Id);
        }
    }
}