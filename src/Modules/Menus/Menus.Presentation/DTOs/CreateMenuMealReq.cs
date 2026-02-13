namespace Menus.Presentation.DTOs
{
    public record CreateMenuMealReqDTO(
        Guid CategoryId,
        string Name,
        string Description,
        string ImgUrl,
        List<Guid> Ingredients,
        List<MealSizeDto> Sizes
        );
    public record MealSizeDto(
        string Name,
        decimal Price,
        int SortOrder,
        List<IngredientQuantityDto> IngredientQuantity
        );
    public record IngredientQuantityDto(
        Guid IngredientId,
        decimal Quantity
        );
}