namespace Subscriptions.Domain.Exceptions
{
    public class SubscriptionHasNoDeliveryDaysException : Exception
    {
        public SubscriptionHasNoDeliveryDaysException() : base("Subscription must have at least one delivery day.") { }
    }
}