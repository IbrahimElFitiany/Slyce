using Shared.Kernal.Exceptions;

namespace Menus.Domain.Exceptions
{
    public class MinimumMealSizesRequiredException : DomainException
    {
        public MinimumMealSizesRequiredException() : base("A meal must have at least one size.") { }
    }
}
