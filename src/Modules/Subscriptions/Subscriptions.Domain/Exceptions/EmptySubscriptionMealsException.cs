using Shared.Domain.Exceptions;

namespace Subscriptions.Domain.Exceptions
{
    internal class EmptySubscriptionMealsException : DomainException
    {
        public EmptySubscriptionMealsException() : base("A subscription must have at least one meal.") { }
    }
}