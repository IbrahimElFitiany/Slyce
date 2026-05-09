using MediatR;
using Asp.Versioning;
using Customers.Application.UseCases.Commands.DeleteCustomer;
using Customers.Application.UseCases.Commands.UpdateGender;
using Customers.Presentation.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Presentation;

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteCustomerCommand(id), cancellationToken);
            return Ok();
        }

        [HttpPatch("me/profile/gender")]
        [Authorize (Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomerGender([FromBody] UpdateGenderRequest request)
        {
            await mediator.Send(new UpdateGenderCommand(UserId, request.Gender));
            return NoContent();
        }

    }
}