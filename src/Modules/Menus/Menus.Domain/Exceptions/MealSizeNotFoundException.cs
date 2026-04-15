using Shared.Domain.Exceptions;

namespace Menus.Domain.Exceptions
{
    public sealed class MealSizeNotFoundException : DomainException
    {
        public MealSizeNotFoundException(Guid sizeId) : base($"Meal Size with Id '{sizeId}' was not found.") { }
    }
}
