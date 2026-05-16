using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.UseCases.Commands.UpdateRestaurantImage;
using Restaurants.Presentation.DTOs;
using Shared.Presentation;

namespace Restaurants.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class RestaurantsController(IMediator mediator) : BaseController
    {

        [HttpPut("{id}/logo")]
        [Authorize(Roles = "RestaurantOwner")]
        public async Task<IActionResult> UpdateRestaurantLogo(
        [FromRoute] Guid id,
        [FromBody] UpdateRestauantLogoRequest request,
        CancellationToken ct)
        {
            //TODO AuthR and 
            await mediator.Send(new UpdateRestaurantImageCommand(id, request.ImageUrl), ct);

            return NoContent();
        }
    }
}
