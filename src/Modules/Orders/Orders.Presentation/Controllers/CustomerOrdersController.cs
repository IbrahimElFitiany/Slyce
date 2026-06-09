using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.UseCases.Queries.GetCustomerOrders;
using Orders.Application.UseCases.Queries.GetOrderById;
using Shared.Presentation;

namespace Orders.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/customers/{customerId:guid}/orders")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(Roles = "Customer")]

    public sealed class CustomerOrdersController(IMediator mediator) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetCustomerOrders([FromRoute] Guid customerId, CancellationToken ct)
        {
            if (customerId != UserId)
                return Forbid();

            var result = await mediator.Send(new GetCustomerOrdersQuery(customerId), ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrder(
            [FromRoute] Guid customerId,
            [FromRoute] Guid Id,
            CancellationToken ct)
        {
            if (customerId != UserId)
                return Forbid();

            var result = await mediator.Send(new GetOrderByIdQuery(Id), ct);
            return Ok(result);
        }
    }
}