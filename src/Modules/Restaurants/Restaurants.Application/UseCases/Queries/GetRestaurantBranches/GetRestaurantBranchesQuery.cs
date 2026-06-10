using MediatR;

namespace Restaurants.Application.UseCases.Queries.GetRestaurantBranches
{
    public sealed record GetRestaurantBranchesQuery(Guid RestaurantId) : IRequest<GetRestaurantBranchesQueryResponse>;
    
    public sealed record GetRestaurantBranchesQueryResponse(IReadOnlyList<RestaurantBranch> Branches);

    public sealed record RestaurantBranch(Guid BranchId, string Name);
}
