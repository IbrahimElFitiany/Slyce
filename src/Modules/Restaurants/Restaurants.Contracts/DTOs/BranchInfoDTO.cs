namespace Restaurants.Contracts.DTOs
{
    public sealed record BranchInfoDTO(Guid BranchId, Guid RestaurantId, string RestaurantName, string? LogoUrl);
}
