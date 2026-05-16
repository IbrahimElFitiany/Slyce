using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Customers.Application.UseCases.Commands.CreateAddress;
using Customers.Presentation.DTOs;
using Customers.Application.UseCases.Queries.GetCustomerAddress;
using Customers.Application.UseCases.Queries.ListCustomerAddresses;
using Shared.Presentation;
using Customers.Application.UseCases.Commands.DeleteAddress;


namespace Customers.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/addresses")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AddressesController(IMediator mediator) : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateAddressCommand(
                CustomerId: UserId,
                Label: request.Label,
                StreetName: request.StreetName,
                StreetNumber: request.StreetNumber,
                Area: request.Area,
                City: request.City,
                Latitude: request.Latitude,
                Longitude: request.Longitude,
                ContactNumber: request.ContactNumber);

            var addressId = await mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetAddress), new { id = addressId }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress( [FromRoute] Guid id,CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetCustomerAddressQuery(UserId, id), cancellationToken);

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteAddressCommand(UserId, id), cancellationToken);
            return NoContent();
        }


        [HttpGet]
        public async Task<IActionResult> ListCustomerAddresses(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new ListCustomerAddressesQuery(UserId), cancellationToken);

            return Ok(result);
        }
    }
}