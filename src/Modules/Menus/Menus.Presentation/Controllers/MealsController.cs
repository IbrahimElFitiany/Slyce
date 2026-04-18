using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Menus.Application.UseCases.Commands.CreateMenuMeal;
using Menus.Presentation.DTOs;
using Menus.Application.UseCases.Queries.GetMealByID;

namespace Menus.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/meals")]
    [ApiVersion("1.0")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MealsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuMeal([FromBody] CreateMenuMealReqDTO dto, CancellationToken ct)
        {
            Guid dummyUserId = Guid.NewGuid();
            Guid mockUpRestaurant = Guid.Parse("ee7a56b2-eded-4745-9f7f-b13a02dce23f");

            var command = new CreateMenuMealCommand(
                dummyUserId,
                mockUpRestaurant,
                dto.CategoryId,
                dto.Name,
                dto.Description,
                dto.ImgUrl,
                dto.Ingredients,
                dto.Sizes.Select(size =>
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

            var mealId = await _mediator.Send(command, ct);

            return CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { id = mealId, version = "1.0" },
                value: new { id = mealId }
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetMealByIdQuery(id), ct); 
            return Ok(result);
        }
    }
}