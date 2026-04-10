using MediatR;

namespace Menus.Application.UseCases.Commands.AddMealSize
{
    public sealed record AddMealSizeCommand(
        Guid MealId,
        string Name,
        decimal Price,
        int SortOrder,
        IEnumerable<IngredientQuantityInput> IngredientQuantities) : IRequest<Guid>;

    public sealed record IngredientQuantityInput(Guid IngredientId, decimal Quantity);
}
