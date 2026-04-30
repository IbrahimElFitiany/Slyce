namespace Shared.Domain.Exceptions
{
    public class DuplicateException : DomainException
    {
        public DuplicateException(string message) : base(message) { }
        public DuplicateException(string entityName, string duplicateValue) : base($"{entityName} with value '{duplicateValue}' already exists") { }
    }
}
