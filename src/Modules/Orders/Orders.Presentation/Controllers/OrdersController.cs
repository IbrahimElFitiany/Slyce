using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.UseCases.Commands.CreateOrder;
using Orders.Application.UseCases.Commands.UpdateOrderStatus;
using Orders.Presentation.DTOs;

namespace Orders.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/orders")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class OrdersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest createOrderRequest, CancellationToken ct)
        {
            var orderId = await _mediator.Send(new CreateOrderCommand(createOrderRequest.CustomerId, Guid.NewGuid(), new List<OrderItemRequest> {}), ct);
            return Created(string.Empty, orderId);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateOrderStatusRequest updateOrder)
        {
            await _mediator.Send(new UpdateOrderStatusCommand(id, updateOrder.Status));
            return Ok();
        }
    }
}