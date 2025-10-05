namespace Restaurants.Domain.Exceptions
{

    public sealed class InvalidRestaurantNameException : DomainException
    {
        public InvalidRestaurantNameException(string? name)
            : base($"Invalid restaurant name: '{name}'") { }
    }
}
