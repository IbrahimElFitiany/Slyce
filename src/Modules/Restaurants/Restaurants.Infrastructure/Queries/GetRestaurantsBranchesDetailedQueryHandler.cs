using Microsoft.EntityFrameworkCore;
using MediatR;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Application.UseCases.Queries;

namespace Restaurants.Infrastructure.Queries
{
    internal sealed class GetRestaurantsBranchesDetailedQueryHandler(RestaurantDbContext dbContext)
        : IRequestHandler<GetRestaurantsBranchesDetailedQuery, IReadOnlyList<RestaurantBranchDetailedResponse>>
    {
        public async Task<IReadOnlyList<RestaurantBranchDetailedResponse>> Handle(
            GetRestaurantsBranchesDetailedQuery request,
            CancellationToken ct)
        {
            return await dbContext.RestaurantBranches
                .AsNoTracking()
                .Where(b => b.RestaurantId == request.RestaurantId)
                .Select(b => new RestaurantBranchDetailedResponse(
                    b.Id,
                    b.Name,
                    b.IsActive,
                    b.Address.StreetName,
                    b.Address.StreetNumber,
                    b.Address.City,
                    b.Address.Area,
                    b.PhoneNumber.Value
                ))
                .ToListAsync(ct);
        }
    }
}