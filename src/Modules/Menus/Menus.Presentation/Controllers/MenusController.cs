using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Menus.Application.UseCases.Queries.GetMenuByRestaurantId;
using Menus.Application.UseCases.Queries.GetMenuForOwner;
using Microsoft.AspNetCore.Authorization;
using Shared.Presentation;


namespace Menus.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/restaurants/{restaurantId}/menu")]
    [ApiVersion("1.0")]
    [ApiController]
    public class MenusController (IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetByRestaurant([FromRoute] Guid restaurantId, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetMenuByRestaurantIdQuery(restaurantId), ct);
            return Ok(result);
        }

        [Authorize (Roles = "RestaurantOwner")]
        [HttpGet("owner")]
        public async Task<IActionResult> GetMenuForOwner()
        {
            var result = await mediator.Send(new GetMenuForOwnerQuery(RestaurantId));
            return Ok(result);
        }

    }
}