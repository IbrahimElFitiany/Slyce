using Microsoft.EntityFrameworkCore;
using MediatR;
using Menus.Application.UseCases.Queries.GetUnreviewedMeals;
using Shared.Application;
using Menus.Infrastructure.Persistence;
using Restaurants.Contracts.Interfaces;

namespace Menus.Infrastructure.Queries
{
    internal sealed class GetUnreviewedMealsQueryHandler(
        MenusDbContext dbContext,
        IRestaurantQueryServices restaurantQueryServices) : IRequestHandler<GetUnreviewedMealsQuery, PagedResult<UnreviewedMealSummary>>
    {
        public async Task<PagedResult<UnreviewedMealSummary>> Handle(GetUnreviewedMealsQuery request, CancellationToken ct)
        {
            var mealsQuery = dbContext.MenuMeals
                .AsNoTracking()
                .Where(m => !m.Reviewed);

            var totalCount = await mealsQuery.CountAsync(ct);

            if (totalCount == 0)
            {
                return PagedResult<UnreviewedMealSummary>.Empty(request.Page, request.PageSize);
            }

            var rawMealsData = await mealsQuery
                .OrderBy(m => m.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(m => new
                {
                    m.Id,
                    m.RestaurantId,
                    m.Name,
                    m.Description,
                    m.Image,
                    SmallestSizeKcal = m.Sizes
                        .OrderBy(s => s.SortOrder)
                        .Select(s => s.Nutrition.Calories)
                        .FirstOrDefault(),
                    m.CreatedAt
                })
                .ToListAsync(ct);

            var restaurantIds = rawMealsData.Select(m => m.RestaurantId).Distinct().ToList();

            var restaurantProfiles = await restaurantQueryServices.GetRestaurantBrandingByIdsAsync(restaurantIds, ct);

            var pendingMeals = rawMealsData.Select(m =>
            {
                var profile = restaurantProfiles[m.RestaurantId];

                return new UnreviewedMealSummary(
                    m.Id,
                    m.RestaurantId,
                    profile.BrandName,
                    profile.Logo,
                    m.Name,
                    m.Description,
                    m.Image,
                    m.SmallestSizeKcal,
                    m.CreatedAt);
            }).ToList();

            int totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            return new PagedResult<UnreviewedMealSummary>(
                totalCount,
                request.PageSize,
                request.Page,
                totalPages,
                request.Page < totalPages,
                request.Page > 1,
                pendingMeals);
        }
    }
}