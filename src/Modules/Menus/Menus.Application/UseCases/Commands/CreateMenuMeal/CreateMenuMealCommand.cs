using MediatR;

namespace Menus.Application.UseCases.Commands.CreateMenuMeal
{
    public record CreateMenuMealCommand(
        // TODO: Decide whether to pass UserId explicitly in the command or use ICurrentUserContext in the handler
        Guid UserId,
        Guid RestaurantId,
        Guid CategoryId,
        string Name,
        string Description,
        string ImgUrl,
        IReadOnlyList<Guid> Ingredients,
        IReadOnlyList<MealSizeInput> Sizes
    ) : IRequest<Guid>;

    public record MealSizeInput(
        string Name,
        decimal Price,
        int SortOrder,
        IReadOnlyList<IngredientQuantityInput> IngredientQuantities
    );

    public record IngredientQuantityInput(
        Guid IngredientId,
        decimal Quantity
    );
}