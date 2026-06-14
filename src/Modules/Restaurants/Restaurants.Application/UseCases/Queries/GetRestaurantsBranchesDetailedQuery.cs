using MediatR;

namespace Restaurants.Application.UseCases.Queries
{
    public sealed record GetRestaurantsBranchesDetailedQuery(Guid RestaurantId) : IRequest<IReadOnlyList<RestaurantBranchDetailedResponse>>;

    public sealed record RestaurantBranchDetailedResponse(
        Guid Id,
        string? Name,
        bool IsActive,
        string? StreetName,
        string? StreetNumber,
        string City,
        string Area,
        string PhoneNumber
    );
}