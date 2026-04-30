namespace Food.Application.UseCases.Queries
{
    public sealed record SearchFoodSummaryQueryResult(
        Guid Id,
        string? ImageUrl,
        string Name,
        decimal Calories,
        decimal Fat,
        decimal Protein,
        decimal Carbs);
}