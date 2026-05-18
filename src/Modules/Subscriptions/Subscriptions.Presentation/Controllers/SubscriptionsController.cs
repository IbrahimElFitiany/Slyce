using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Presentation;
using Subscriptions.Application.UseCases.Commands.CreateSubscription;
using Subscriptions.Application.UseCases.Queries.GetCustomerSubscriptionsSummary;
using Subscriptions.Presentation.DTOs;


namespace Subscriptions.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/subscriptions")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class SubscriptionsController(IMediator mediator) : BaseController
    {

        [HttpPost]
        [Authorize (Roles = "Customer")]
        public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionRequest request, CancellationToken ct)
        {
            var command = new CreateSubscriptionCommand(
                UserId,
                request.BranchId,
                request.DeliveryAddressId,
                request.TimeSlot,
                request.DeliveryDays,
                request.StartDate,
                request.SubscriptionMeals
                .Select(sm => new SubscriptionMealInput(sm.MealSizeId,sm.Quantity)),
                request.BillingCycle);

            var result = await mediator.Send(command,ct);

            return CreatedAtAction(nameof(GetSubscription), new { id = result }, new { SubscriptionId = result });
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCustomerSubscriptions([FromQuery] GetCustomerSubscriptionsRequest request, CancellationToken ct)
        {
            var query = new GetCustomerSubscriptionsSummaryQuery(
                UserId,
                request.Status,
                request.SortDescending,
                request.Page,
                request.PageSize);

            var results = await mediator.Send(query, ct);

            return Ok(results);
        }

        [HttpGet ("{id}")]
        public Task<IActionResult> GetSubscription([FromRoute] Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}