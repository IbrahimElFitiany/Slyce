using MediatR;
using Asp.Versioning;
using Menus.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Menus.Application.UseCases.Commands.CreateMenuCategory;


namespace WebAPI.Controllers
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
        public async Task<IActionResult> Create([FromRoute] Guid restaurantId,[FromBody] DTO tO , CancellationToken ct)
        {
            var dto = new CreateMenuCategoryReqDTO(tO.Name , restaurantId);

            await _mediator.Send(new CreateMenuCategoryCommand(dto), ct);
            return Ok();
        }

        public record DTO (string Name);

    }
}