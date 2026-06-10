using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurants.Application.UseCases.Queries.GetRestaurantBranches;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Queries
{
    internal sealed class GetRestaurantBranchesQueryHandler(
        RestaurantDbContext dbContext) : IRequestHandler<GetRestaurantBranchesQuery, GetRestaurantBranchesQueryResponse>
    {
        public async Task<GetRestaurantBranchesQueryResponse> Handle(GetRestaurantBranchesQuery query, CancellationToken cancellationToken)
        {
            var result = await dbContext.RestaurantBranches
                .Where(b => b.RestaurantId == query.RestaurantId)
                .Select(b => new RestaurantBranch(b.Id, b.Name))
                .ToListAsync(cancellationToken);

            return new GetRestaurantBranchesQueryResponse(result);
        }
    }
}
