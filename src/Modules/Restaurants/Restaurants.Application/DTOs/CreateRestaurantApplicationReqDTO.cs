using Restaurants.Domain.Enums;

namespace Restaurants.Application.DTOs
{
    public sealed record CreateRestaurantApplicationReqDTO(
        string BrandName,
        string OwnerFirstName,
        string OwnerLastName,
        string CompanyEmail,
        string MobileNumber,
        RestaurantType RestaurantType,
        int Branches = 1,
        string? Description = null
    );
}
