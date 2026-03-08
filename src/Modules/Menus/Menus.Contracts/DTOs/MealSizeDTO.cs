namespace Menus.Contracts.DTOs
{
    public sealed record MealSizeDTO(
        Guid MealId,
        Guid MealSizeId,
        decimal PriceAtSubscription);
}
