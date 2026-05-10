using MediatR;
using Asp.Versioning;
using Customers.Presentation.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Presentation;
using Customers.Application.UseCases.Commands.UpdateWeight;
using Customers.Application.UseCases.Commands.UpdateGender;

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
        public async Task<IActionResult> UpdateCustomerGender([FromBody] UpdateGenderRequest request)
        {
            await mediator.Send(new UpdateGenderCommand(UserId, request.Gender));
            return NoContent();
        }

        [HttpPatch("me/profile/weight")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomerWeight([FromBody] UpdateWeightRequest request)
        {
            await mediator.Send(new UpdateWeightCommand(UserId, request.WeightKg));
            return NoContent();
        }

    }
}