using Shared.Domain.Exceptions;

namespace Menus.Domain.Exceptions
{
    public class MealSizesLimitExceededException : DomainException
    {

        public MealSizesLimitExceededException(int limit) : base($"A meal cannot have more than {limit} sizes.") { }
    }
}
