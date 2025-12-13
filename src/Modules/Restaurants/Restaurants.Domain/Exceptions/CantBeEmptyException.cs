namespace Restaurants.Domain.Exceptions
{

    public sealed class CantBeEmptyException : DomainException
    {
        public CantBeEmptyException(string fieldName)
            : base($"{fieldName} can't be empty") { }
    }
}
