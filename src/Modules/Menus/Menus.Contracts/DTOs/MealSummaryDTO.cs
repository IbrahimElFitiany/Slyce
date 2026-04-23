namespace Menus.Contracts.DTOs
{
    public sealed record MealSummaryDTO(
        Guid RestaurantId,
        Guid MealId,
        IReadOnlyList<Guid> MealSizeIds);
}
