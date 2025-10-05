using Restaurants.Domain.Enums;

namespace Restaurants.Application.DTOs
{
    public sealed record RegisterRestaurantRequest(
        string Name,
        string Description,
        string? Image,
        RestaurantType Type
    );
}
