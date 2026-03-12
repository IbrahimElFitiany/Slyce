using Shared.Domain.Exceptions;

namespace Subscriptions.Domain.Exceptions
{
    public sealed class InvalidSubscriptionDateException : DomainException
    {
        public InvalidSubscriptionDateException(DateOnly startDate): base($"Subscription start date '{startDate}' cannot be in the past.") { }
    }
}
