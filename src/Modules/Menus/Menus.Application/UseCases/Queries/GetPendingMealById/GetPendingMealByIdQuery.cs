using MediatR;

namespace Menus.Application.UseCases.Queries.GetPendingMealById
{
    public record GetPendingMealByIdQuery(Guid Id) : IRequest<GetPendingMealByIdResponse>;
    public record GetPendingMealByIdResponse(
        Guid Id,
        string Name,
        string Description,
        string Image,
        bool Reviewed,
        DateTime CreatedAt,
        List<MealIngredientDTO> Ingredients,
        List<MealSizeDTO> Sizes);

    public record MealIngredientDTO(Guid FoodId, string Name);
    public record MealSizeDTO(
        Guid Id,
        string Name,
        decimal Price,
        int SortOrder,
        decimal Calories);
}