using Customers.Application.Interfaces;
using Customers.Domain.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace SlyceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        public CustomerController(ILogger<CustomerController> logger , ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            _logger.LogInformation("User {UserId} placed an order at {Time}",123 , DateTime.UtcNow);
            var customers = await _customerService.ListCustomers();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer()
        {
            Customer newCustomer = new Customer("Ibrahim", "El-Fitiany", Gender.M , DateOnly.Parse("2004-11-25"));
            await _customerService.AddCustomer(newCustomer);

            _logger.LogInformation("User {UserId} placed an order at {Time}", newCustomer.Id , DateTime.UtcNow);
            return Ok(newCustomer);
        }

        public class UpdateEmailRequest
        {
            public Guid CustomerId { get; set; }
            public string NewEmail { get; set; }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailRequest request)
        {
            await _customerService.UpdateEmailAsync(request.CustomerId, request.NewEmail);
            return Ok();
        }
    }
}
