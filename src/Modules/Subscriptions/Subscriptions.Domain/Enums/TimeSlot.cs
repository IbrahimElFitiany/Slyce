using Subscriptions.Domain.ValueObjects;

namespace Subscriptions.Domain.Enums
{
    public enum TimeSlot
    {
        EarlyMorning,
        Morning,
        Afternoon,
        LateAfternoon,
        Evening,
        Night
    }

    public static class TimeSlotExtensions
    {
        private static readonly Dictionary<TimeSlot, DeliveryTimeFrame> _slots = new()
        {
            { TimeSlot.EarlyMorning,  new DeliveryTimeFrame(new TimeOnly(6,  0), new TimeOnly(9,  0)) },
            { TimeSlot.Morning,       new DeliveryTimeFrame(new TimeOnly(9,  0), new TimeOnly(12, 0)) },
            { TimeSlot.Afternoon,     new DeliveryTimeFrame(new TimeOnly(12, 0), new TimeOnly(15, 0)) },
            { TimeSlot.LateAfternoon, new DeliveryTimeFrame(new TimeOnly(15, 0), new TimeOnly(18, 0)) },
            { TimeSlot.Evening,       new DeliveryTimeFrame(new TimeOnly(18, 0), new TimeOnly(21, 0)) },
            { TimeSlot.Night,         new DeliveryTimeFrame(new TimeOnly(21, 0), new TimeOnly(23, 59)) },
        };

        public static DeliveryTimeFrame ToDeliveryTimeFrame(this TimeSlot timeFrame) => _slots[timeFrame];
    }
}
