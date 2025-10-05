using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.DTOs;
using Restaurants.Application.UseCases;
using Restaurants.Domain.Entities;
using WebAPI.Responses;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly RegisterRestaurantUseCase _registerRestaurantUseCase;
        public RestaurantsController(RegisterRestaurantUseCase registerRestaurantUseCase) {
            _registerRestaurantUseCase = registerRestaurantUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterRestaurant(
            [FromBody] RegisterRestaurantRequest registerRestaurant,
            CancellationToken cancellationToken)
        {
            var restaurant = await _registerRestaurantUseCase.ExecuteAsync(registerRestaurant,cancellationToken);
            return Created(string.Empty, ApiResponse<RegisterRestaurantResponse>.SuccessResponse(restaurant, "Created Successfully", 201));

        }
    }
}
