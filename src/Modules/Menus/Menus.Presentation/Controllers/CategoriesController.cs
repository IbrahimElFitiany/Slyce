using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Menus.Application.UseCases.Commands.CreateMenuCategory;
using Menus.Presentation.DTOs;
using Shared.Presentation;
using Menus.Application.UseCases.Queries.GetRestaurantCategories;
using Microsoft.AspNetCore.Authorization;


namespace Menus.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/restaurants/{restaurantId}/categories")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class CategoriesController(IMediator mediator) : BaseController
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

        [HttpGet]
        [Authorize (Roles = "RestaurantOwner")]
        public async Task<IActionResult> GetCategories([FromRoute] Guid restaurantId, CancellationToken ct)
        {
            if (restaurantId != RestaurantId)
                return Forbid();

            var result = await mediator.Send(new GetRestaurantCategoriesQuery(restaurantId), ct);
            return Ok(result);
        }
    }
}