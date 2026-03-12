using Shared.Domain.Exceptions;

namespace Subscriptions.Domain.Exceptions
{
    public class DeliveryScheduleConflictException : DomainException
    {
        public DeliveryScheduleConflictException(DayOfWeek day) : base($"Delivery schedule conflicts with restaurant operating hours on {day}."){ }
    }
}
