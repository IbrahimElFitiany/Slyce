using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Menus.Presentation.DTOs;
using Menus.Application.UseCases.Commands.AddMealSize;
using Menus.Application.UseCases.Commands.RemoveMealSize;

namespace Menus.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/meals")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SizesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{mealId}/sizes")]
        public async Task<IActionResult> AddSize([FromRoute] Guid mealId ,[FromBody] MealSizeDto request, CancellationToken ct)
        {
            var command = new AddMealSizeCommand(
                MealId: mealId,
                Name: request.Name,
                Price: request.Price,
                SortOrder: request.SortOrder,
                IngredientQuantities: request.IngredientQuantities
                .Select(iq => new IngredientQuantityInput(iq.IngredientId, iq.Quantity)));

            var sizeId = await _mediator.Send(command, ct);
            return Ok(new { id = sizeId });
        }

        [HttpDelete("{mealId}/sizes/{sizeId}")]
        public async Task<IActionResult> RemoveSize([FromRoute] Guid mealId, [FromRoute] Guid sizeId, CancellationToken ct)
        {
            await _mediator.Send(new RemoveMealSizeCommand(mealId,sizeId), ct);
            return Ok();
        }
    }
}