using Menus.Domain.ValueObjects;
using Shared.Domain.ValueObjects;

namespace Menus.Domain.Services
{
    public static class NutritionCalculator
    {
        public static Nutrition CalculateForSize( IEnumerable<IngredientQuantity> ingredientQuantities, IDictionary<Guid, Nutrition> nutritionByIngredient)
        {
            var totalNutrition = Nutrition.Zero();

            foreach (var ingredientQuantity in ingredientQuantities)
            {
                var ingredientNutrition = nutritionByIngredient[ingredientQuantity.MealIngredientId];

                var scaled = ingredientNutrition.Multiply(ingredientQuantity.Quantity / 100m);

                totalNutrition += scaled;
            }

            return totalNutrition;

        }
    }
}