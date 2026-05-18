namespace Food.Application.UseCases.Queries.SearchFoodSummary
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