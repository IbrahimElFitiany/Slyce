using MediatR;

namespace Restaurants.Application.UseCases.Commands.AddBranch
{
    public sealed record AddBranchCommand(
        Guid RestaurantId,
        string BranchName,
        string BranchContactNumber,
        BranchAddressInput Address) : IRequest<Guid>;

    public sealed record BranchAddressInput(
        string City,
        string Area,
        string? StreetName,
        string? StreetNumber,
        double Latitude,
        double Longitude);
}