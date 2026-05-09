using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.UseCases.Commands.ApproveRestaurantApplication;
using Restaurants.Application.UseCases.Commands.CreateRestaurantApplication;
using Restaurants.Presentation.DTOs;
using Shared.Presentation;

namespace Restaurants.Presentation.Controllers
{
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/restaurant-applications")]
    [ApiVersion("1.0")]
    [ApiController]
    public sealed class RestaurantApplicationsController(IMediator mediator) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateRestaurantApplication([FromBody] CreateRestaurantApplicationRequest dto, CancellationToken cancellationToken)
        {
            var command = new CreateRestaurantApplicationCommand(
                BrandName: dto.BrandName,
                OwnerFirstName: dto.OwnerFirstName,
                OwnerLastName: dto.OwnerLastName,
                CompanyEmail: dto.CompanyEmail,
                OwnerMobileNumber: dto.OwnerMobileNumber,
                CompanyMobileNumber: dto.CompanyMobileNumber,
                RestaurantType: dto.RestaurantType,
                MainBranchAddress: new MainBranchAddressInput(
                    City: dto.MainBranchAddress.City,
                    Area: dto.MainBranchAddress.Area,
                    StreetName: dto.MainBranchAddress.StreetName,
                    StreetNumber: dto.MainBranchAddress.StreetNumber,
                    Latitude: dto.MainBranchAddress.Latitude,
                    Longitude: dto.MainBranchAddress.Longitude
                ),
                BranchCount: dto.BranchCount,
                Description: dto.Description
                );
            var result = await mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = result }, new { id = result });
        }

        [HttpPost("{applicationId}/approve")]
        public async Task<IActionResult> ApproveRestaurantApplication([FromRoute] Guid applicationId, CancellationToken cancellationToken)
        { 
            await mediator.Send(new ApproveRestaurantApplicationCommand(UserId, applicationId));

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            await Task.Delay(19);
            return Ok(new { });
        }
    }
}