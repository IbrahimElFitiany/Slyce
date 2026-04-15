using MediatR;

namespace Menus.Application.UseCases.Commands.RemoveMealSize
{
    public sealed record RemoveMealSizeCommand (Guid MealId, Guid SizeId) : IRequest;
}
