namespace Subscriptions.Domain.ValueObjects
{
    public sealed record BranchSchedule
    {
        public IReadOnlyDictionary<DayOfWeek, TimeRange> OperatingHours { get; init; }

        public BranchSchedule(Dictionary<DayOfWeek, TimeRange> operatingHours)
        {
            OperatingHours = operatingHours;
        }

        public bool IsOpenOn(DayOfWeek day) => OperatingHours.ContainsKey(day);

        public bool IsOpenDuring(DayOfWeek day, TimeRange timeFrame) => IsOpenOn(day) && OperatingHours[day].Contains(timeFrame);

    }

    public record TimeRange(TimeOnly Start, TimeOnly End)
    {
        public bool Contains(TimeRange other) => other.Start >= Start && other.End <= End;
    }
}

