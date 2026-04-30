using Asp.Versioning;
using Food.Application.UseCases.Commands.ImportExternalFood;
using Food.Application.UseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Food.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class FoodController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> SearchFood (
            [FromQuery] string term,
            [FromQuery] int pageSize,
            [FromQuery] int pageNumber,
            CancellationToken ct)
        {

            var result = await _mediator.Send(new SearchFoodSummaryQuery(term, pageNumber, pageSize), ct);

            return Ok(result);
        }

        [HttpPost ("external")]
        public async Task<IActionResult> FetchExternalFood([FromQuery] string searchTerm, CancellationToken ct)
        {
            var result = await _mediator.Send(new ImportExternalFoodsCommand(searchTerm), ct);
            return Ok(result);
        }
    }
}
