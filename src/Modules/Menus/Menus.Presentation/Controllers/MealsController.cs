using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Menus.Application.UseCases.Commands.CreateMenuMeal;
using Menus.Presentation.DTOs;
using Menus.Application.UseCases.Queries.GetMealByID;
using Shared.Presentation;
using Menus.Application.UseCases.Queries.GetUnreviewedMeals;
using Microsoft.AspNetCore.Authorization;
using Menus.Application.UseCases.Queries.GetPendingMealById;
using Menus.Application.UseCases.Commands.ApproveMenuMeal;
using Microsoft.AspNetCore.Http;

namespace Menus.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/meals")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class MealsController(IMediator mediator) : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> CreateMenuMeal(
            [FromBody] CreateMenuMealRequest request,
            [FromHeader(Name = "X-Restaurant-Id")] Guid mockUpRestaurant,
            CancellationToken ct)
        {

            var command = new CreateMenuMealCommand(
                UserId,
                mockUpRestaurant,
                request.CategoryId,
                request.Name,
                request.Description,
                request.ImgUrl,
                request.Ingredients,
                request.Sizes.Select(size =>
                    new MealSizeInput(
                        size.Name,
                        size.Price,
                        size.SortOrder,
                        size.IngredientQuantities.Select(iq =>
                            new IngredientQuantityInput(
                                iq.IngredientId,
                                iq.Quantity
                            )
                        ).ToList()
                    )
                ).ToList()
            );

            var mealId = await mediator.Send(command, ct);

            return CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { id = mealId, version = "1.0" },
                value: new { id = mealId }
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetMealByIdQuery(id), ct); 
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("pending/{id}")]
        public async Task<IActionResult> GetPendingMeal([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetPendingMealByIdQuery(id), ct);

            return Ok(result);
        }


        [HttpPatch("pending/{id:guid}/approve")]
        public async Task<IActionResult> Approve(Guid id, CancellationToken cancellationToken)
        {
            await mediator.Send(new ApproveMenuMealCommand(id), cancellationToken);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingMeals(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetUnreviewedMealsQuery(page, pageSize), ct);
            return Ok(result);
        }
    }
}