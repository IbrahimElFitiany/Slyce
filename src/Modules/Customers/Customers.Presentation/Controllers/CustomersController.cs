using Asp.Versioning;
using Customers.Application.UseCases.Commands.DeleteCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class CustomersController (IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


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