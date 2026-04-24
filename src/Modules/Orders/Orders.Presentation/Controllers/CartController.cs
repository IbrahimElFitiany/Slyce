using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.UseCases.Commands.AddToCart;
using Orders.Application.UseCases.Commands.CheckoutCart;
using Orders.Application.UseCases.Queries.ViewCart;
using Orders.Presentation.DTOs;


namespace Orders.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/cart")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class CartController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> AddToCart(
            [FromHeader (Name = "X-CustomerId")] Guid customerId,
            [FromBody] AddToCartReq request,
            CancellationToken ct)
        {
            var command = new AddToCartCommand(
                customerId,
                request.MealId,
                request.SizeId,
                request.Quantity);

            await _mediator.Send(command, ct);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ViewCart([FromHeader(Name = "X-CustomerId")] Guid customerId, CancellationToken ct)
        {
            var result = await _mediator.Send(new ViewCartQuery(customerId), ct);
            return Ok(result);
        }

    }

}
