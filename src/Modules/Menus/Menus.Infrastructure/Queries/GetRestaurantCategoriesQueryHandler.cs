using MediatR;
using Menus.Application.UseCases.Queries.GetRestaurantCategories;
using Menus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Menus.Infrastructure.Queries
{
    internal sealed class GetRestaurantCategoriesQueryHandler(
        MenusDbContext dbContext) : IRequestHandler<GetRestaurantCategoriesQuery, GetRestaurantCategoriesQueryResponse>
    {
        public async Task<GetRestaurantCategoriesQueryResponse> Handle(GetRestaurantCategoriesQuery query, CancellationToken cancellationToken)
        {
            var result = await dbContext.MenuCategories
                .Where(c => c.RestaurantId == query.RestaurantId)
                .Select(c => new CategoryItem(c.Id, c.Name))
                .ToListAsync(cancellationToken);

            return new GetRestaurantCategoriesQueryResponse(result);
        }
    }
}
