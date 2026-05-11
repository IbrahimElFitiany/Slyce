using MediatR;
using Asp.Versioning;
using Customers.Presentation.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Presentation;
using Customers.Application.UseCases.Commands.UpdateWeight;
using Customers.Application.UseCases.Commands.UpdateGender;
using Customers.Application.UseCases.Commands.UpdateHeight;
using Customers.Application.UseCases.Commands.UpdateActivityRate;

namespace Customers.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class CustomersController (IMediator mediator) : BaseController
    {

        [HttpGet("{id}")]
        public Task<IActionResult> GetCustomer([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("me/profile/gender")]
        [Authorize (Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomerGender([FromBody] UpdateGenderRequest request, CancellationToken ct)
        {
            await mediator.Send(new UpdateGenderCommand(UserId, request.Gender), ct);
            return NoContent();
        }

        [HttpPatch("me/profile/weight")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomerWeight([FromBody] UpdateWeightRequest request, CancellationToken ct)
        {
            await mediator.Send(new UpdateWeightCommand(UserId, request.WeightKg), ct);
            return NoContent();
        }

        [HttpPatch("me/profile/height")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomerheight([FromBody] UpdateHeightRequest request, CancellationToken ct)
        {
            await mediator.Send(new UpdateHeightCommand(UserId, request.HeightCm), ct);
            return NoContent();
        }

        [HttpPatch("me/profile/activity-rate")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomerActivityRate([FromBody] UpdateActivityRateRequest request, CancellationToken ct)
        {
            await mediator.Send(new UpdateActivityRateCommand(UserId, request.ActivityRate), ct);
            return NoContent();
        }

    }
}