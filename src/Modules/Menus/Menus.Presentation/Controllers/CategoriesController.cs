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
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromRoute] Guid restaurantId,
            [FromBody] CreateCategoryReq createCategoryReq,
            CancellationToken ct)
        {
            var categoryId = await _mediator.Send(new CreateMenuCategoryCommand(createCategoryReq.Name,restaurantId), ct);
            return Created(string.Empty, new { categoryId });
        }
    }
}