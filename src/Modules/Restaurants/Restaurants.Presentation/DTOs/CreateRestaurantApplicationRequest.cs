namespace Restaurants.Presentation.DTOs
{
    public sealed record CreateRestaurantApplicationRequest(
        string BrandName,
        string OwnerFirstName,
        string OwnerLastName,
        string CompanyEmail,
        string OwnerEmail,
        string OwnerMobileNumber,
        string CompanyMobileNumber,
        string RestaurantType,
        MainBranchAddress MainBranchAddress,
        int BranchCount = 1,
        string? Description = null);

    public sealed record MainBranchAddress(
        string City,
        string Area,
        string? StreetName,
        string? StreetNumber,
        double Latitude,
        double Longitude);
}