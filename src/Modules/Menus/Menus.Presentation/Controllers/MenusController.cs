using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Menus.Application.UseCases.Queries.GetMenuByRestaurantId;


namespace Menus.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/restaurants/{restaurantId}/menu")]
    [ApiVersion("1.0")]
    [ApiController]
    public class MenusController (IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetByRestaurant([FromRoute] Guid restaurantId, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetMenuByRestaurantIdQuery(restaurantId), ct);
            return Ok(result);
        }

    }
}