using MediatR;
using Menus.Application.UseCases.Queries.GetMenuForOwner;
using Menus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Menus.Infrastructure.Queries
{
    internal sealed class GetMenuForOwnerQueryHandler(
        MenusDbContext menusDbContext) : IRequestHandler<GetMenuForOwnerQuery, GetMenuForOwnerQueryResponse>
    {
        public async Task<GetMenuForOwnerQueryResponse> Handle(GetMenuForOwnerQuery query, CancellationToken ct)
        {
            var menu = await menusDbContext.MenuCategories
                .Where(c => c.RestaurantId == query.RestaurantId)
                .Select(c => new OwnerMenuCategoryDTO(
                    c.Id,
                    c.Name,
                    menusDbContext.MenuMeals
                        .Where(m => m.CategoryId == c.Id)
                        .Select(m => new OwnerMenuMealDTO(
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
                            m.Image,
                            m.Reviewed
                        )).ToList()
                ))
                .ToListAsync(ct);

            return new GetMenuForOwnerQueryResponse(menu);
        }
    }
}