namespace Shared.Application.Exceptions
{
    public class DuplicateException : ApplicationException
    {
        public DuplicateException(string message) : base(message) { }
        public DuplicateException(string entityName, string duplicateValue) : base($"{entityName} with value '{duplicateValue}' already exists") { }
    }
}
