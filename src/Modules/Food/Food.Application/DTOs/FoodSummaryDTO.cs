namespace Food.Application.DTOs
{
    public sealed record FoodSummaryDTO(
        Guid Id,
        string ImageUrl,
        string Name,
        decimal Calories,
        decimal Fat,
        decimal Protien,
        decimal Carbs);
}
