using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.UseCases.Commands.CreateBranchSchedule;
using Restaurants.Presentation.DTOs;


namespace Restaurants.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/branches")]
    [ApiVersion("1.0")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BranchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id}/working-hours")]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateBranchScheduleRequest request, [FromRoute] Guid id, CancellationToken ct)
        {
            var command = new CreateBranchScheduleCommand(
                id,
                request.Schedule
                .Select(d => new DayWorkingHoursInput(d.Day, d.OpeningTime, d.ClosingTime)).ToList());

            await _mediator.Send(command,ct);

            return CreatedAtAction(nameof(GetSchedule), new { id }, null);
        }

        [HttpGet ("{id}/working-hours")]
        public Task<IActionResult> GetSchedule([FromRoute] Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}