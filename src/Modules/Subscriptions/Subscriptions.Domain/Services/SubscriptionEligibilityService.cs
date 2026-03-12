using Shared.Domain.ValueObjects;
using Subscriptions.Domain.Exceptions;
using Subscriptions.Domain.Interfaces;
using Subscriptions.Domain.ValueObjects;

namespace Subscriptions.Domain.Services
{
    public class SubscriptionEligibilityService
    {
        private readonly IDistanceCalculator _distanceCalculator;

        public SubscriptionEligibilityService(IDistanceCalculator distanceCalculator)
        {
            _distanceCalculator = distanceCalculator;
        }

        public void EnsureScheduleMatches(DeliveryTimeFrame deliveryTimeFrame, IEnumerable<DayOfWeek> deliveryDays, BranchSchedule branchSchedule)
        {
            foreach (var day in deliveryDays)
            {
                if (!branchSchedule.IsOpenDuring(day, new TimeRange(deliveryTimeFrame.From, deliveryTimeFrame.To)))
                    throw new DeliveryScheduleConflictException(day);
            }
        }

        public async Task EnsureCustomerIsWithinRadius(Coordinates customerCoordinates,Coordinates branchCoordinates)
        {
            var isWithin = await _distanceCalculator.LocationWithinLocationRadius(customerCoordinates, branchCoordinates);

            if (!isWithin)
                throw new CustomerOutOfDeliveryRadiusException();
        }
    }
}