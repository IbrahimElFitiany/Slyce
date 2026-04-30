using Shared.Domain.Exceptions;

namespace Menus.Domain.Exceptions
{
    public class MealSizeMustHaveIngredientsException : DomainException
    {
        public MealSizeMustHaveIngredientsException() : base("A meal size must have at least one ingredient.") { }
    }
}
