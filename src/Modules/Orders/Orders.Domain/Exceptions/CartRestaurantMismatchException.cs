using Shared.Domain.Exceptions;

namespace Orders.Domain.Exceptions
{
    public sealed class CartRestaurantMismatchException : DomainException
    {
        public CartRestaurantMismatchException() : base("Cannot mix meals from different restaurants in the same cart.") { }

        public CartRestaurantMismatchException(Guid cartRestaurantId, Guid mealRestaurantId) : base($"Cart restaurant '{cartRestaurantId}' does not match meal restaurant '{mealRestaurantId}'."){ }
    }
}