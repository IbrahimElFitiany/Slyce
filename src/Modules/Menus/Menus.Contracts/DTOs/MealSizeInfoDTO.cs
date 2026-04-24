namespace Menus.Contracts.DTOs
{
    public sealed record MealSizeInfoDTO(
        Guid MealId,
        Guid SizeId,
        string MealName,
        string SizeName,
        string? Description,
        string ImageUri,
        decimal Price,
        decimal? DiscountedPrice,
        string Currency,
        decimal Calories);
}
