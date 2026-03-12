using Shared.Domain.Exceptions;

namespace Subscriptions.Domain.Exceptions
{
    internal class CustomerOutOfDeliveryRadiusException : DomainException
    {
        public CustomerOutOfDeliveryRadiusException() : base("Customer location is outside the branch delivery radius."){ }
    }
}