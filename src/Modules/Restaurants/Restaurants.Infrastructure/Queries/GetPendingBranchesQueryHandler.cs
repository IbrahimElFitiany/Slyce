using Microsoft.EntityFrameworkCore;
using MediatR;
using Restaurants.Application.UseCases.Queries.GetPendingBranches;
using Restaurants.Infrastructure.Persistence;
using Shared.Application;

namespace Restaurants.Infrastructure.Queries
{
    internal sealed class GetPendingBranchesQueryHandler(RestaurantDbContext dbContext)
        : IRequestHandler<GetPendingBranchesQuery, PagedResult<PendingBranchSummary>>
    {
        public async Task<PagedResult<PendingBranchSummary>> Handle(GetPendingBranchesQuery request, CancellationToken ct)
        {
            var branchesQuery = dbContext.RestaurantBranches
                .AsNoTracking()
                .Where(b => !b.IsActive);

            var totalCount = await branchesQuery.CountAsync(ct);

            if (totalCount == 0)
            {
                return PagedResult<PendingBranchSummary>.Empty(request.Page, request.PageSize);
            }

            var pendingBranches = await branchesQuery
                .Join(
                    dbContext.Restaurants.AsNoTracking(),
                    branch => branch.RestaurantId,
                    restaurant => restaurant.Id,
                    (branch, restaurant) => new { branch, restaurant }
                )
                .OrderBy(x => x.branch.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PendingBranchSummary(
                    x.branch.Id,
                    x.branch.RestaurantId,
                    x.restaurant.BrandName,
                    x.restaurant.Logo,
                    x.restaurant.Banner,
                    x.branch.Name,
                    x.branch.Address.City,
                    x.branch.Address.Area,
                    x.branch.Address.Coordinates.Latitude,
                    x.branch.Address.Coordinates.Longitude,
                    x.branch.CreatedAt
                ))
                .ToListAsync(ct);

            int totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            return new PagedResult<PendingBranchSummary>(
                totalCount,
                request.PageSize,
                request.Page,
                totalPages,
                request.Page < totalPages,
                request.Page > 1,
                pendingBranches);
        }
    }
}