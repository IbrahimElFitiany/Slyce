using MediatR;

namespace Menus.Application.UseCases.Queries.SearchMealsNearBy
{
    public sealed record SearchNearbyMealsQuery(string SearchTerm, double Lng , double Lat) : IRequest<SearchMealsNearByResponse>;
    public sealed record SearchMealsNearByResponse(IReadOnlyList<MealSummary> Meals);
    public sealed record MealSummary(
        Guid MealId,
        string MealImageUrl,
        string MealName,
        double LowestCalorieOption,
        string Currency,
        decimal Price);

}