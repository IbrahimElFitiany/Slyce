namespace Menus.Application.UseCases.Queries.GetMealByID
{
    public sealed record GetMealByIdQueryResponse(
        Guid MealId,
        string Name,
        string Description,
        string ImageUri,
        bool Avalible,
        IReadOnlyList<MealSizeResponse> Sizes);

    public sealed record MealSizeResponse(
        Guid SizeId,
        string SizeName,
        decimal Price,
        string Currency,
        int SortOrder,
        IReadOnlyList<MealIngredientResponse> Ingredients,
        NutritionResponse Nutrition);
    public sealed record MealIngredientResponse(string IngredientName,decimal Qty);
    public sealed record NutritionResponse(
        decimal Calories,
        decimal Protein,
        decimal TotalFat,
        decimal SaturatedFat,
        decimal TransFat,
        decimal Cholesterol,
        decimal SodiumMg,
        decimal TotalCarbohydrate,
        decimal DietaryFiber,
        decimal SugarGrams,
        decimal VitaminD,
        decimal CalciumMg,
        decimal IronMg,
        decimal PotassiumMg,
        decimal VitaminAMcg,
        decimal VitaminCMg);
}
