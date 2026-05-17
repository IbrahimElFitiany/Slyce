using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.UseCases.Commands.ActivateBranch;
using Restaurants.Application.UseCases.Commands.AddBranch;
using Restaurants.Application.UseCases.Commands.CreateBranchSchedule;
using Restaurants.Application.UseCases.Queries.GetBranchDetails;
using Restaurants.Presentation.DTOs;


namespace Restaurants.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/branches")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class BranchController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddBranch([FromBody] AddBranchRequest request, CancellationToken ct)
        {
            var command = new AddBranchCommand(
                RestaurantId: request.RestaurantId,
                BranchName: request.BranchName,
                BranchContactNumber: request.BranchContactNumber,
                Address: new BranchAddressInput(
                    City: request.City,
                    Area: request.Area,
                    StreetNumber: request.StreetNumber,
                    StreetName: request.StreetName,
                    Latitude: request.Latitude,
                    Longitude: request.Longitude));

            var result = await mediator.Send(command, ct);

            return Created(string.Empty ,new { branchId = result });
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetBranchDetails([FromRoute] Guid id, CancellationToken ct)
        {
            return Ok(await mediator.Send(new GetBranchDetailsQuery(id), ct));
        }

        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> ActivateBranch([FromRoute] Guid id, CancellationToken ct)
        {
            //TODO AuthR and 
            await mediator.Send(new ActivateBranchCommand(id), ct);

            return NoContent();
        }

        [HttpPost("{id}/working-hours")]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateBranchScheduleRequest request, [FromRoute] Guid id, CancellationToken ct)
        {
            var command = new CreateBranchScheduleCommand(
                id,
                request.Schedule
                .Select(d => new DayWorkingHoursInput(d.Day, d.OpeningTime, d.ClosingTime)).ToList());

            await mediator.Send(command,ct);

            return CreatedAtAction(nameof(GetSchedule), new { id }, null);
        }

        [HttpGet ("{id}/working-hours")]
        public Task<IActionResult> GetSchedule([FromRoute] Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}