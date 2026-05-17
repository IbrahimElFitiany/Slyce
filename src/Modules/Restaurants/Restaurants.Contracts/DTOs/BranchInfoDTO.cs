namespace Restaurants.Contracts.DTOs
{
    public sealed record BranchInfoDTO(Guid BranchId, string RestaurantName, string LogoUrl);
}
