using Customers.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAPI.RequestModels;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeycloakWebhookController : ControllerBase
    {
        private readonly ILogger<KeycloakWebhookController> _logger;

        public KeycloakWebhookController(
            ILogger<KeycloakWebhookController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveEvent(KeycloakEventRequest keycloakEvent)
        {
            _logger.LogInformation("Received Keycloak webhook: {EventType} for User {UserId}", keycloakEvent.Type, keycloakEvent.UserId);

            switch (keycloakEvent.Type?.ToLower())
            {
                case "register":
                    await HandleUserRegistration(keycloakEvent);
                    break;

                case "create":
                    await HandleUserRegistration(keycloakEvent);
                    break;

                //case "delete":
                //    await HandleUserDeletion(keycloakEvent.UserId);
                //    break;

                default:
                    return BadRequest($"Unknown event type: {keycloakEvent.Type}");
            }
            
            _logger.LogInformation("Successfully processed Keycloak webhook: {EventType} for User {UserId}", keycloakEvent.Type, keycloakEvent.UserId);
            return Ok();
        }

        private async Task HandleUserRegistration(KeycloakEventRequest keycloakEvent)
        {
            if (keycloakEvent.Fname == null || keycloakEvent.Lname == null || keycloakEvent.Email == null) throw new Exception("Info is missing");
            //await _registerCustomerUseCase.Execute(keycloakEvent.UserId, keycloakEvent.Fname, keycloakEvent.Lname, keycloakEvent.Email); 
        }
        //private async Task HandleUserDeletion(string userId)
        //{
        //    await _deleteCustomerUseCase.Execute(userId);
        //}
    }
}