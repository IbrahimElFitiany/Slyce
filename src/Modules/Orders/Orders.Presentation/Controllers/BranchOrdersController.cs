using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.UseCases.Queries.GetBranchOrders;
using Orders.Application.UseCases.Queries.GetOrderById;
using Shared.Presentation;

namespace Orders.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/branches/{branchId:guid}/orders")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(Roles = "RestaurantOwner")]
    public sealed class BranchOrdersController(IMediator mediator) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetBranchOrders(
            [FromRoute] Guid branchId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetBranchOrdersQuery(branchId, page, pageSize), ct);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranchOrderById(
            [FromRoute] Guid branchId,
            [FromRoute] Guid id,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetOrderByIdQuery(id), ct);
            return Ok(result);
        }
    }
}