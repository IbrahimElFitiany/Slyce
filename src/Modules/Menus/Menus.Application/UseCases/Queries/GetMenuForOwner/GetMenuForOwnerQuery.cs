using MediatR;

namespace Menus.Application.UseCases.Queries.GetMenuForOwner
{
    public sealed record GetMenuForOwnerQuery(Guid RestaurantId) : IRequest<GetMenuForOwnerQueryResponse>;

    public sealed record GetMenuForOwnerQueryResponse(IReadOnlyCollection<OwnerMenuCategoryDTO> Menu);

    public sealed record OwnerMenuCategoryDTO(
        Guid CategoryId,
        string Name,
        IReadOnlyCollection<OwnerMenuMealDTO> Meals);

    public sealed record OwnerMenuMealDTO(
        Guid MealId,
        string Name,
        string Description,
        decimal OriginalPrice,
        decimal? DiscountedPrice,
        string PriceCurrency,
        decimal LowestCalorieOption,
        string ImgUrl,
        bool Reviewed);

}
