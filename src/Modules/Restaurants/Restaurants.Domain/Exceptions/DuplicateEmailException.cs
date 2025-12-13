namespace Restaurants.Domain.Exceptions
{

    public sealed class DuplicateEmailException : DomainException
    {
        public DuplicateEmailException(string? email)
            : base($"Duplicate Email: '{email}'") { }
    }
}
