using Asp.Versioning;
using Identity.Application.UseCases.Commands.Login;
using Identity.Application.UseCases.Commands.RegisterCustomer;
using Identity.Presentation.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class UsersController (IMediator mediator) : ControllerBase
    {

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

            var result = await mediator.Send(command, ct);

            return Created(string.Empty, new { CustomerId = result} );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request,
            [FromHeader(Name = "X-Client-Type")] string? clientType,
            CancellationToken ct)
        {
            var result = await mediator.Send(new LoginCommand(request.Email, request.Password), ct);

            if (string.Equals(clientType, "mobile", StringComparison.OrdinalIgnoreCase))
                return Ok(new { accessToken = result.AccessToken, expiresAt = result.ExpiresAt });

            Response.Cookies.Append("access_token", result.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = result.ExpiresAt,
                Path = "/"
            });

            return NoContent();
        }
    }
}