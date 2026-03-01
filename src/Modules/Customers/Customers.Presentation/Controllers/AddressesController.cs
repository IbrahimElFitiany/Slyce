using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Customers.Application.UseCases.Commands.CreateAddress;
using Customers.Presentation.DTOs;



namespace Customers.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/addresses")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AddressesController(IMediator _mediator) : ControllerBase
    {
        
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressRequest request, CancellationToken cancellationToken)
        {
            // TODO: replace with authenticated user's ID once auth is implemented
            var mockCustomer = Request.Headers["Customer-Id"].FirstOrDefault();

            if (mockCustomer is null || !Guid.TryParse(mockCustomer, out var customerId))
                return BadRequest("Customer-Id header is required");

            var command = new CreateAddressCommand(
                CustomerId: customerId,
                Label: request.Label,
                StreetName: request.StreetName,
                StreetNumber: request.StreetNumber,
                Area: request.Area,
                City: request.City,
                Latitude: request.Latitude,
                Longitude: request.Longitude,
                ContactNumber: request.ContactNumber);

            var addressId = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetAddress), new { id = addressId }, null);
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetAddress([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


    }
}