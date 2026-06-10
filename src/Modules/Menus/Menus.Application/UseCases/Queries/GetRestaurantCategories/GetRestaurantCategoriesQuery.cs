using MediatR;

namespace Menus.Application.UseCases.Queries.GetRestaurantCategories
{
    public sealed record GetRestaurantCategoriesQuery(Guid RestaurantId) : IRequest<GetRestaurantCategoriesQueryResponse>;

    public sealed record GetRestaurantCategoriesQueryResponse(IReadOnlyList<CategoryItem> Categories);
    public sealed record CategoryItem(Guid CategoryId, string Name);
}
