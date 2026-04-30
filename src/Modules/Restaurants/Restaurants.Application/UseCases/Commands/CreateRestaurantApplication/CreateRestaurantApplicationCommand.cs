using MediatR;

namespace Restaurants.Application.UseCases.Commands.CreateRestaurantApplication
{
    public sealed record CreateRestaurantApplicationCommand(
        string BrandName,
        string OwnerFirstName,
        string OwnerLastName,
        string CompanyEmail,
        string OwnerMobileNumber,
        string CompanyMobileNumber,
        string RestaurantType,
        MainBranchAddressInput MainBranchAddress,
        int BranchCount = 1,
        string? Description = null) : IRequest<Guid>;

    public sealed record MainBranchAddressInput(
        string City,
        string Area,
        string? StreetName,
        string? StreetNumber,
        double Latitude,
        double Longitude);
}