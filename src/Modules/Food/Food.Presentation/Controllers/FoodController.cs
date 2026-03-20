using Asp.Versioning;
using Food.Application.UseCases.Commands.ImportExternalFood;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Application;

namespace Food.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class FoodController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public FoodController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost ("external")]
        public async Task<IActionResult> FetchExternalFood([FromQuery] string searchTerm, CancellationToken ct)
        {
            var result = await _mediator.Send(new ImportExternalFoodsCommand(searchTerm), ct);
            return Ok(result);
        }
    }
}
