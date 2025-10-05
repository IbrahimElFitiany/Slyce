

using Restaurants.Domain.Entities;

namespace Restaurants.Application.DTOs
{
    public record RegisterRestaurantResponse(
        Guid id,
        string Name,
        string Description,
        string RestaurantType,
        string Status
    );
}
