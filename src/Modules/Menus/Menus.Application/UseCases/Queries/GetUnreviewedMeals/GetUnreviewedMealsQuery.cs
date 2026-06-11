using MediatR;
using Shared.Application;

namespace Menus.Application.UseCases.Queries.GetUnreviewedMeals
{
    public sealed record GetUnreviewedMealsQuery(int Page, int PageSize) : IRequest<PagedResult<UnreviewedMealSummary>>;
    public sealed record UnreviewedMealSummary(
        Guid MealId,
        Guid RestaurantId,
        string RestaurantName,
        string RestaurantLogo,
        string MealName,
        string MealDescription,
        string MealImage,
        decimal SmallestSizeKcal,
        DateTime CreatedAt);
}