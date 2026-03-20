namespace Food.Application.DTOs
{
    public sealed record ExternalFoodResultDTO(
        string Name,
        string ImageUrl,
        string ExternalId,
        string Source,
        decimal Calories,
        decimal Protein,
        decimal Carbs,
        decimal Fat,
        decimal SaturatedFat,
        decimal TransFat,
        decimal Cholesterol,
        decimal SodiumMg,
        decimal DietaryFiber,
        decimal SugarGrams,
        decimal PotassiumMg,
        decimal CalciumMg,
        decimal IronMg,
        decimal VitaminAMcg,
        decimal VitaminCMg,
        decimal VitaminD);
}
