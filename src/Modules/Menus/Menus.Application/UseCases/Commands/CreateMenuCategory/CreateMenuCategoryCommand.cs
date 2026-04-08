using MediatR;

namespace Menus.Application.UseCases.Commands.CreateMenuCategory
{
    public record CreateMenuCategoryCommand(string Name, Guid RestaurantId) : IRequest<Guid>;
}