using MediatR;

namespace Menus.Application.UseCases.Commands.ApproveMenuMeal
{
    public sealed record ApproveMenuMealCommand(Guid MealId) : IRequest;
}
