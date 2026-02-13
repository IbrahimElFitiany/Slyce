using Shared.Domain.Exceptions;

namespace Menus.Domain.Exceptions
{
    public class MealSizeIngredientMismatchException : DomainException
    {
        public MealSizeIngredientMismatchException(string sizeName) : base($"Meal size '{sizeName}' ingredients do not match the meal's ingredients.") { }
    }
}
