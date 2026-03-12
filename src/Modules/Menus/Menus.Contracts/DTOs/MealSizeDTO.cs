namespace Menus.Contracts.DTOs
{
    public sealed record MealSizeDTO(
        Guid MealId,
        string MealName,
        Guid MealSizeId,
        string SizeName,
        decimal PriceAmountAtSubscription,
        string PriceCurrency
        );
}
