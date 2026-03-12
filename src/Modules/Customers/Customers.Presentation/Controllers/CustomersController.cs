using Asp.Versioning;
using Customers.Application.UseCases.Commands.DeleteCustomer;
using Customers.Application.UseCases.Commands.RegisterCustomer;
using Customers.Presentation.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var command = new RegisterCustomerCommand(
                Fname: request.Fname,
                Lname: request.Lname,
                Email: request.Email,
                PhoneNumber: request.PhoneNumber,
                BirthDay: request.BirthDay);
            var customerId = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetCustomer),new {id = customerId}, new {Id = customerId});
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetCustomer([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCustomerCommand(id), cancellationToken);
            return Ok();
        }

    }
}
