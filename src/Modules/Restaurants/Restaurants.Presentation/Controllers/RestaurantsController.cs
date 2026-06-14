using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.UseCases.Commands.ActivateRestaurant;
using Restaurants.Application.UseCases.Commands.UpdateRestaurantBanner;
using Restaurants.Application.UseCases.Commands.UpdateRestaurantImage;
using Restaurants.Application.UseCases.Queries;
using Restaurants.Application.UseCases.Queries.GetNearbyTopRatedRestaurants;
using Restaurants.Application.UseCases.Queries.GetRestaurants;
using Restaurants.Domain.Enums;
using Restaurants.Presentation.DTOs;
using Shared.Presentation;

namespace Restaurants.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class RestaurantsController(IMediator mediator) : BaseController
    {
        [HttpGet("{id}/branches")]
        [Authorize(Roles = "RestaurantOwner")]
        public async Task<IActionResult> GetRestaurantBranchesDetailed([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetRestaurantsBranchesDetailedQuery(id), ct);
            return Ok(result);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRestaurants(
        [FromQuery] RestaurantStatus? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetRestaurantsQuery(status, page, pageSize), ct);
            return Ok(result);
        }

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

        [HttpPost("{id}/activate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRestaurantBanner([FromRoute] Guid id, CancellationToken ct)
        {
            await mediator.Send(new ActivateRestaurantCommand(id), ct);
            return NoContent();
        }

        [HttpPut("{id}/banner")]
        [Authorize(Roles = "RestaurantOwner")]
        public async Task<IActionResult> UpdateRestaurantBanner(
            [FromRoute] Guid id,
            [FromBody] UpdateRestauantBannerRequest request,
            CancellationToken ct)
        {
            await mediator.Send(new UpdateRestaurantBannerCommand(id, request.BannerUrl), ct);
            return NoContent();
        }

        [HttpGet("top-rated/nearby")]
        public async Task<IActionResult> GettopRated([FromBody] GetNearbyTopRatedRestaurantsRequest request, CancellationToken ct)
        {
            var branches = await mediator.Send(new GetNearbyTopRatedRestaurantsQuery(request.Latitude, request.Longitude), ct);

            return Ok(branches);
        }
    }
}