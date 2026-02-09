using Shared.Kernal.Exceptions;

namespace Menus.Domain.Exceptions
{
    public class DuplicateMealSizeException : DomainException
    {
        public DuplicateMealSizeException() : base("A meal size with this name already exists.") { }

        public DuplicateMealSizeException(string sizeName) : base ($"A meal size with the name '{sizeName}' already exists.") { }
    }
}
