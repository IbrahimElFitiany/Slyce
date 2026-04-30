using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Customers.Application.UseCases.Commands.CreateAddress;
using Customers.Presentation.DTOs;
using Customers.Application.UseCases.Queries.GetCustomerAddress;
using Customers.Application.UseCases.Queries.ListCustomerAddresses;


namespace Customers.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/addresses")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AddressesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

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
        public async Task<IActionResult> GetAddress(
            [FromHeader(Name = "Customer-Id")] Guid customerId,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            //TODO Customer-Id is Used For mocking rn

            var query = new GetCustomerAddressQuery(customerId, id); 

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ListCustomerAddresses(
            [FromHeader(Name = "Customer-Id")] Guid customerId,
            CancellationToken cancellationToken)
        {
            //TODO Customer-Id is Used For mocking rn

            var query = new ListCustomerAddressesQuery(customerId);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}