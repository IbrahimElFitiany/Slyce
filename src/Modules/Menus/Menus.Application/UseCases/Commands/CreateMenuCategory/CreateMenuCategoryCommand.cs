using MediatR;

namespace Menus.Application.UseCases.Commands.CreateMenuCategory
{
    public record CreateMenuCategoryCommand(string name, Guid restaurantId) : IRequest;
}