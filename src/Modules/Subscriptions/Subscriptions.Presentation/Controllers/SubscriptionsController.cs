using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Application.UseCases.Commands.CreateSubscription;
using Subscriptions.Presentation.DTOs;


namespace Subscriptions.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/subscriptions")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubscriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription(
            [FromHeader] Guid Customer_Id,
            [FromBody] CreateSubscriptionRequest request,
            CancellationToken ct)
        {
            //TODO: add valid customer instead of the mockCustomer

            var command = new CreateSubscriptionCommand(
                Customer_Id,
                request.BranchId,
                request.DeliveryAddressId,
                request.TimeSlot,
                request.DeliveryDays,
                request.StartDate,
                request.SubscriptionMeals
                .Select(sm => new SubscriptionMealInput(sm.MealSizeId,sm.Quantity)),
                request.BillingCycle);

            var result = await _mediator.Send(command,ct);

            return CreatedAtAction(nameof(GetSubscription), new { id = result }, new { SubscriptionId = result });
        }

        [HttpGet ("{id}")]
        public Task<IActionResult> GetSubscription([FromRoute] Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}