using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.UseCases.Commands.UpdateOrderStatus;
using Orders.Presentation.DTOs;
using Shared.Presentation;

namespace Orders.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class OrdersController(IMediator mediator) : BaseController
    {
        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateOrderStatusRequest request)
        {
            await mediator.Send(new UpdateOrderStatusCommand(id, request.Status));
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder ([FromRoute] Guid id)
        {
            return Ok();
        }
    }
}