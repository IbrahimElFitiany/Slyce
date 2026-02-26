namespace Shared.Domain.Exceptions
{
    public sealed class InvalidDomainOperationException : DomainException
    {
        public InvalidDomainOperationException(string message) : base(message) { }
    }
}
