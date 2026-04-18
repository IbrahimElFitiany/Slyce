using MediatR;

namespace Menus.Application.UseCases.Queries.GetMealByID
{
    public sealed record GetMealByIdQuery(Guid MealId):IRequest<GetMealByIdQueryResponse>;
}
