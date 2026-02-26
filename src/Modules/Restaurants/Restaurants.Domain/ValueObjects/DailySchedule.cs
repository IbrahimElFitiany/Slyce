namespace Restaurants.Domain.ValueObjects
{
    public sealed record DailySchedule
    {
        public DayOfWeek Day { get; }
        public OperatingHours OperatingHours { get; } = null!;

        private DailySchedule() {}

        public DailySchedule(DayOfWeek day, OperatingHours operatingHours)
        {
            Day = day;
            OperatingHours = operatingHours;
        }
    }
}
