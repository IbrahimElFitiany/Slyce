using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.DTOs;
using Restaurants.Application.UseCases.Commands.CreateRestaurantApplication;
using WebAPI.Responses;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/restaurant-applications")]
    [ApiVersion("1.0")]
    [ApiController]
    public class RestaurantApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RestaurantApplicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurantApplication([FromBody] CreateRestaurantApplicationReqDTO dto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateRestaurantApplicationCommand(dto),cancellationToken);

            return Ok(ApiResponse<CreateRestaurantApplicationResDTO>.SuccessResponse(result, "Application submitted successfully", 201));
        }
    }
}