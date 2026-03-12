namespace Subscriptions.Domain.ValueObjects
{
    public sealed record DeliveryTimeFrame
    {
        public TimeOnly From { get; }
        public TimeOnly To { get; }

        public DeliveryTimeFrame(TimeOnly from, TimeOnly to)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(to, from);
            From = from;
            To = to;
        }
    }
}