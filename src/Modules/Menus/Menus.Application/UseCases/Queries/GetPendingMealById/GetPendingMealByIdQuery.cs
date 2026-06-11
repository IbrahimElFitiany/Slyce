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
        List<string> Ingredients,
        List<MealSizeDTO> Sizes);

    public record MealSizeDTO(
        Guid Id,
        string Name,
        decimal Price,
        int SortOrder,
        decimal Calories);
}