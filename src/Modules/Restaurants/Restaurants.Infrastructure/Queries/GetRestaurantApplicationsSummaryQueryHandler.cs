using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurants.Application.UseCases.Queries.GetRestaurantApplicationsSummary;
using Restaurants.Domain.Enums;
using Restaurants.Infrastructure.Persistence;
using Shared.Application;

namespace Restaurants.Infrastructure.Queries
{
    internal sealed class GetRestaurantApplicationsSummaryQueryHandler(RestaurantDbContext restaurantDbContext) : IRequestHandler<GetRestaurantApplicationsSummaryQuery, PagedResult<RestaurantApplicationResult>>
    {
        public async Task<PagedResult<RestaurantApplicationResult>> Handle( GetRestaurantApplicationsSummaryQuery request, CancellationToken ct)
        {
            var query = restaurantDbContext.RestaurantApplications.AsNoTracking();

            if (request.Status is not null)
                query = query.Where(ra => ra.Status == Enum.Parse<ApplicationStatus>(request.Status));

            if (request.From is not null)
                query = query.Where(ra => ra.SubmittedAt >= request.From.Value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc));

            if (request.To is not null)
                query = query.Where(ra => ra.SubmittedAt <= request.To.Value.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc));

            query = request.SortDescending ? query.OrderByDescending(ra => ra.SubmittedAt) : query.OrderBy(ra => ra.SubmittedAt);

            var totalCount = await query.CountAsync(ct);

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(ra => new RestaurantApplicationResult(
                    ra.Id,
                    ra.BrandName,
                    ra.Status.ToString(),
                    ra.SubmittedAt))
                .ToListAsync(ct);

            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            return new PagedResult<RestaurantApplicationResult>(
                Total: totalCount,
                PerPage: request.PageSize,
                CurrentPage: request.Page,
                TotalPages: totalPages,
                HasNext: request.Page < totalPages,
                HasPrevious: request.Page > 1,
                Items: items);
        }
    }
}
