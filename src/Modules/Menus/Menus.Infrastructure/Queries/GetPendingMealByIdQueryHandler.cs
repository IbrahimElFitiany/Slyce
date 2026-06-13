using MediatR;
using Microsoft.EntityFrameworkCore;
using Menus.Application.UseCases.Queries.GetPendingMealById;
using Menus.Infrastructure.Persistence;
using Shared.Application.Exceptions;

namespace Menus.Infrastructure.Queries
{
    internal sealed class GetPendingMealByIdQueryHandler(MenusDbContext dbContext) : IRequestHandler<GetPendingMealByIdQuery, GetPendingMealByIdResponse>
    {
        public async Task<GetPendingMealByIdResponse> Handle(GetPendingMealByIdQuery request, CancellationToken ct)
        {
            var result = await dbContext.MenuMeals
                .AsNoTracking()
                .Where(m => m.Id == request.Id)
                .Select(m => new GetPendingMealByIdResponse(
                    m.Id,
                    m.Name,
                    m.Description,
                    m.Image,
                    m.Reviewed,
                    m.CreatedAt,
                    m.Ingredients.Select(i => new MealIngredientDTO(i.FoodId, i.Name)).ToList(),
                    m.Sizes.OrderBy(s => s.SortOrder).Select(s => new MealSizeDTO(
                        s.Id,
                        s.Name,
                        s.Price.Amount,
                        s.SortOrder,
                        s.Nutrition.Calories
                    )).ToList()
                ))
                .FirstOrDefaultAsync(ct)
                ?? throw new NotFoundException("Meal", request.Id);

            return result;
        }
    }
}