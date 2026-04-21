namespace Menus.Application.UseCases.Queries.GetMenuByRestaurantId
{
    public sealed record GetMenuByRestaurantIdQueryResponse (IReadOnlyCollection<MenuCategoryDTO> Menu);
    public sealed record MenuCategoryDTO(
        Guid CategoryId,
        string Name,
        IReadOnlyCollection<MenuMealDTO> Meals);
    public sealed record MenuMealDTO(
        Guid MealId,
        string Name,
        string Description,
        decimal OriginalPrice,
        decimal? DiscountedPrice,
        string PriceCurrency,
        decimal LowestCalorieOption,
        string ImgUrl);

}