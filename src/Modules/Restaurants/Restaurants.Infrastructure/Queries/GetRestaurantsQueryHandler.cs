using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurants.Application.UseCases.Queries.GetRestaurants;
using Restaurants.Infrastructure.Persistence;
using Shared.Application;

namespace Restaurants.Infrastructure.Queries
{
    internal sealed class GetRestaurantsQueryHandler(RestaurantDbContext db)
        : IRequestHandler<GetRestaurantsQuery, PagedResult<RestaurantSummaryResponse>>
    {
        public async Task<PagedResult<RestaurantSummaryResponse>> Handle(
            GetRestaurantsQuery request,
            CancellationToken cancellationToken)
        {
            var query = db.Restaurants
                .AsNoTracking()
                .AsQueryable();

            if (request.Status.HasValue)
                query = query.Where(r => r.Status == request.Status.Value);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(r => new RestaurantSummaryResponse(
                    r.Id,
                    r.BrandName,
                    r.OwnerId,
                    r.Status.ToString(),
                    db.RestaurantBranches
                        .Where(b => b.RestaurantId == r.Id)
                        .Select(b => b.Address.City)
                        .FirstOrDefault() ?? string.Empty,
                    r.CreatedAt))
                .ToListAsync(cancellationToken);

            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            return new PagedResult<RestaurantSummaryResponse>(
                totalCount,
                request.PageSize,
                request.Page,
                totalPages,
                request.Page < totalPages,
                request.Page > 1,
                items);
        }
    }
}