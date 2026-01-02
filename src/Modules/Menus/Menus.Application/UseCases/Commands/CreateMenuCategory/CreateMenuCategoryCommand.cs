using Menus.Application.DTOs;
using MediatR;

namespace Menus.Application.UseCases.Commands.CreateMenuCategory
{
    public record CreateMenuCategoryCommand(CreateMenuCategoryReqDTO dto) : IRequest;
}