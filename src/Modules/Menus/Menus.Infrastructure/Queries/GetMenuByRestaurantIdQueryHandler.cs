using MediatR;
using Menus.Application.UseCases.Queries.GetMenuByRestaurantId;
using Menus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Menus.Infrastructure.Queries
{
    internal sealed class GetMenuByRestaurantIdQueryHandler (MenusDbContext menusDbContext) : IRequestHandler<GetMenuByRestaurantIdQuery, GetMenuByRestaurantIdQueryResponse>
    {
        private readonly MenusDbContext _menusDbContext = menusDbContext;

        public async Task<GetMenuByRestaurantIdQueryResponse> Handle(GetMenuByRestaurantIdQuery query, CancellationToken ct)
        {
            var menuFromDb = await _menusDbContext.MenuCategories
                .Where(c => c.RestaurantId == query.RestaurantId)
                .Select(c => new MenuCategoryDTO(
                    c.Id,
                    c.Name,
                    _menusDbContext.MenuMeals
                        .Where(m => m.CategoryId == c.Id)
                        .Select(m => new MenuMealDTO(
                            m.Id,
                            m.Name,
                            m.Description,
                            m.Sizes
                                .OrderBy(s => s.SortOrder)
                                .Select(s => s.Price.Amount)
                                .FirstOrDefault(),
                            null,
                            "EGP",
                            m.Sizes
                                .OrderBy(s => s.SortOrder)
                                .Select(s => s.Nutrition.Calories)
                                .FirstOrDefault(),
                            m.Image
                        )).ToList()
                ))
                .ToListAsync(ct);

            return new GetMenuByRestaurantIdQueryResponse(menuFromDb);
        }
    }
}