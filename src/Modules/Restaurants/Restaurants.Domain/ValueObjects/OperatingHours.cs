namespace Restaurants.Domain.ValueObjects
{
    public sealed record OperatingHours
    {
        public TimeOnly OpeningTime { get; }
        public TimeOnly ClosingTime { get; }

        public OperatingHours (TimeOnly openingTime , TimeOnly closingTime)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(closingTime,openingTime,nameof(closingTime));

            OpeningTime = openingTime;
            ClosingTime = closingTime;
        }
    };
  
}
