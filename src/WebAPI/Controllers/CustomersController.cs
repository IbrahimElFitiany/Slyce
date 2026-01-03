using Customers.Application.DTOs;
using Customers.Application.UseCases.Commands.RegisterCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.DTOs;
using System.Timers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerReqDTO req, CancellationToken cancellationToken)
        {
            await _mediator.Send(new RegisterCustomerCommand(req),cancellationToken);
            return Ok();
        }

    }
}
