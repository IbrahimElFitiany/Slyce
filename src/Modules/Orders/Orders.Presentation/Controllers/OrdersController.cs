using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.UseCases.Commands.UpdateOrderStatus;
using Orders.Application.UseCases.Queries.GetOrdersTrendByBranch;
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
        public async Task<IActionResult> UpdateStatus(
            [FromRoute] Guid id,
            [FromBody] UpdateOrderStatusRequest request,
            CancellationToken cancellationToken)
        {
            await mediator.Send(new UpdateOrderStatusCommand(id, request.Status), cancellationToken);
            return NoContent();
        }

        [Authorize(Roles = "Admin, RestaurantOwner")]
        [HttpGet("branch/{id:guid}/summary")]
        public async Task<IActionResult> GetOrdersTrendByBranchId(
            [FromRoute] Guid id,
            [FromQuery] string period,
            CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<PeriodType>(period, ignoreCase: true, out var parsedPeriod))
                parsedPeriod = PeriodType.Today;

            var result = await mediator.Send(new GetOrdersTrendByBranchQuery(id, parsedPeriod), cancellationToken);
            return Ok(result);
        }
    }
}