namespace Food.Contracts.DTOs
{
    public record FoodNutritionDTO (
        Guid Id,
        string Name,
        decimal CalciumMg,
        decimal Calories,
        decimal Cholesterol,
        decimal DietaryFiber,
        decimal IronMg,
        decimal PotassiumMg,
        decimal Protein,
        decimal SaturatedFat,
        decimal SodiumMg,
        decimal SugarGrams,
        decimal TotalCarbohydrate,
        decimal TotalFat,
        decimal TransFat,
        decimal VitaminAMcg,
        decimal VitaminCMg,
        decimal VitaminD
        );
}
