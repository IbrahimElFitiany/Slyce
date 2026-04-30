using Menus.Domain.ValueObjects;
using Shared.Domain.ValueObjects;

namespace Menus.Domain
{
    public sealed record MealSizeCreationInput(
        string Name,
        Price Price,
        int SortOrder,
        IEnumerable<IngredientQuantity> Quantities,
        Nutrition SizeNutrition);
}
