using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace SlyceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        private readonly ILogger<DummyController> _logger;
        public DummyController(ILogger<DummyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetThing()
        {
            _logger.LogInformation("User {UserId} placed an order at {Time}",123 , DateTime.UtcNow);
            return Ok();
        }
    }
}
