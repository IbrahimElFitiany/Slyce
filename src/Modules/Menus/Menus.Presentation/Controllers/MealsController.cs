using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Menus.Application.UseCases.Commands.CreateMenuMeal;
using Menus.Presentation.DTOs;
using Menus.Application.UseCases.Queries.GetMealByID;
using Shared.Presentation;

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
    }
}