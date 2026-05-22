namespace Restaurants.Presentation.DTOs
{
    public sealed record GetNearbyTopRatedRestaurantsRequest(double Latitude, double Longitude);
}