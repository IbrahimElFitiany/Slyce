using MediatR;
using Microsoft.EntityFrameworkCore;
using Menus.Application.UseCases.Queries.SearchMealsNearBy;
using Menus.Infrastructure.Persistence;
using Restaurants.Contracts.Interfaces;
using System.Linq;

namespace Menus.Infrastructure.Queries
{
    internal sealed class SearchNearbyMealsQueryHandler(
        MenusDbContext dbContext,
        IRestaurantQueryServices restaurantQueryServices) : IRequestHandler<SearchNearbyMealsQuery, SearchMealsNearByResponse>
    {
        public async Task<SearchMealsNearByResponse> Handle(SearchNearbyMealsQuery request, CancellationToken cancellationToken)
        {
            var nearbyBranches = await restaurantQueryServices.GetNearByBranchesAsync(request.Lat, request.Lng, cancellationToken);

            if (nearbyBranches == null || !nearbyBranches.Any())
            {
                return new SearchMealsNearByResponse([]);
            }

            var restaurantIds = nearbyBranches.Select(b => b.RestaurantId).Distinct().ToList();

            var query = dbContext.MenuMeals
                .AsNoTracking()
                .Where(m => restaurantIds.Contains(m.RestaurantId));

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                string searchPattern = $"%{request.SearchTerm.Trim()}%";
                query = query.Where(m => EF.Functions.ILike(m.Name, searchPattern));
            }

            var meals = await query
                .Select(m => new
                {
                    m.Id,
                    m.Image,
                    m.Name,
                    LowestCalSize = m.Sizes
                        .OrderBy(s => s.Nutrition.Calories)
                        .Select(s => new { s.Nutrition.Calories, s.Price })
                        .FirstOrDefault()
                })
                .Select(x => new MealSummary(
                    x.Id,
                    x.Image,
                    x.Name,
                    x.LowestCalSize != null ? (double)x.LowestCalSize.Calories : 0.0,
                    x.LowestCalSize != null ? x.LowestCalSize.Price.Currency : string.Empty,
                    x.LowestCalSize != null ? x.LowestCalSize.Price.Amount : 0m
                ))
                .ToListAsync(cancellationToken);

            return new SearchMealsNearByResponse(meals);
        }
    }
}