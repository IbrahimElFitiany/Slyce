namespace Restaurants.Presentation.DTOs
{
    public sealed record AddBranchRequest(
        Guid RestaurantId,
        string BranchName,
        string BranchContactNumber,
        string City,
        string Area,
        string? StreetNumber,
        string? StreetName,
        double Latitude,
        double Longitude);
}