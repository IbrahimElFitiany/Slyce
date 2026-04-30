namespace Restaurants.Contracts.DTOs
{
    public sealed record BranchForSubscription(
        Guid RestaurantId,
        double Longitude,
        double Latitude,
        IDictionary<DayOfWeek, TimeFrame> Schedule);

    public record TimeFrame(TimeOnly OpeningHour, TimeOnly ClosingHour);
}
