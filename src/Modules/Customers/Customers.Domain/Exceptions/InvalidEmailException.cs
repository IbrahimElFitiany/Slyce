namespace Customers.Domain.Exceptions
{
    public class InvalidEmailException : DomainException
    {
        public InvalidEmailException(string email) : base($"The email '{email}' is invalid.") { }
    }
}