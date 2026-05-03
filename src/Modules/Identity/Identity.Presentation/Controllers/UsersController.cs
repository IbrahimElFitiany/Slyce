using Asp.Versioning;
using Identity.Application.UseCases.Commands.RegisterCustomer;
using Identity.Presentation.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class UsersController (IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("customers")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerRequest request, CancellationToken ct)
        {
            var command = new RegisterCustomerCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password,
                request.PhoneNumber,
                request.BirthDay
            );

            var result = await _mediator.Send(command, ct);

            return Created(string.Empty, new { CustomerId = result} );
        }
    }
}