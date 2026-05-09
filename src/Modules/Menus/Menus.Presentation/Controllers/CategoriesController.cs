using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Menus.Application.UseCases.Commands.CreateMenuCategory;
using Menus.Presentation.DTOs;


namespace Menus.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/restaurants/{restaurantId}/categories")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class CategoriesController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromRoute] Guid restaurantId,
            [FromBody] CreateCategoryReq createCategoryReq,
            CancellationToken ct)
        {
            var categoryId = await mediator.Send(new CreateMenuCategoryCommand(createCategoryReq.Name,restaurantId), ct);
            return Created(string.Empty, new { categoryId });
        }
    }
}