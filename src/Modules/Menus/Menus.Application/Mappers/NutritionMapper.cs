using Food.Contracts.DTOs;
using Shared.Domain.ValueObjects;

namespace Menus.Application.Mappers
{
    internal static class NutritionMapper
    {
        public static Dictionary<Guid, Nutrition> ToNutrition(Dictionary<Guid, FoodNutritionDTO> nutritionData)
        {
            return nutritionData.ToDictionary(
                kvp => kvp.Key,
                kvp => ToNutrition(kvp.Value)
            );
        }

        public static Nutrition ToNutrition(FoodNutritionDTO dto) {

            return new Nutrition(
                    protein: dto.Protein,
                    carb: dto.TotalCarbohydrate,
                    fat: dto.TotalFat,
                    calories: dto.Calories,
                    saturatedFat: dto.SaturatedFat,
                    transFat: dto.TransFat,
                    cholesterol: dto.Cholesterol,
                    sodiumMg: dto.SodiumMg,
                    dietaryFiber: dto.DietaryFiber,
                    sugarGrams: dto.SugarGrams,
                    vitaminD: dto.VitaminD,
                    calciumMg: dto.CalciumMg,
                    ironMg: dto.IronMg,
                    potassiumMg: dto.PotassiumMg,
                    vitaminA_Mcg: dto.VitaminAMcg,
                    vitaminC_Mg: dto.VitaminCMg);
        }
    }
}

