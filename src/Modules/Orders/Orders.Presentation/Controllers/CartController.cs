using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.UseCases.Commands.AddToCart;
using Orders.Application.UseCases.Commands.CheckoutCart;
using Orders.Application.UseCases.Commands.ClearCart;
using Orders.Application.UseCases.Commands.UpdateCartItemQuantity;
using Orders.Application.UseCases.Queries.ViewCart;
using Orders.Presentation.DTOs;
using Shared.Presentation;


namespace Orders.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/cart")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public sealed class CartController(IMediator mediator) : BaseController
    {

        [HttpPatch("items/quantity")]
        public async Task<IActionResult> UpdateCartItemQuantity([FromBody] UpdateCartItemQuantityRequest request, CancellationToken ct)
        {
            await mediator.Send(new UpdateCartItemQuantityCommand(UserId, request.MealId, request.SizeId, request.Quantity), ct);
            return NoContent();
        }

        public sealed record UpdateCartItemQuantityRequest(Guid MealId, Guid SizeId, int Quantity);


        [HttpPost]
        public async Task<IActionResult> AddToCart(
            [FromBody] AddToCartReq request,
            CancellationToken ct)
        {
            var command = new AddToCartCommand(
                UserId,
                request.BranchId,
                request.MealId,
                request.SizeId,
                request.Quantity);

            await mediator.Send(command, ct);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ViewCart(CancellationToken ct)
        {
            var result = await mediator.Send(new ViewCartQuery(UserId), ct);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart(CancellationToken ct)
        {
            await mediator.Send(new ClearCartCommand(UserId), ct);
            return NoContent();
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(
            CheckoutReq request,
            CancellationToken ct)
        {
            var command = new CheckoutCartCommand(UserId, request.DeliveryAddressId, request.PaymentMethod);
            await mediator.Send(command, ct);

            return NoContent();
        }

    }

}