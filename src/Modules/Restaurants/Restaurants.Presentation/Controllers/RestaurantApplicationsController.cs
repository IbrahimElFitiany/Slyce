using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.UseCases.Commands.ApproveRestaurantApplication;
using Restaurants.Application.UseCases.Commands.CreateRestaurantApplication;
using Restaurants.Presentation.DTOs;

namespace Restaurants.Presentation.Controllers
{
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/restaurant-applications")]
    [ApiVersion("1.0")]
    [ApiController]
    public class RestaurantApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RestaurantApplicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

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
            var result = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = result }, new { id = result });
        }

        [HttpPost("approve/{applicationId}")]
        public async Task<IActionResult> ApproveRestaurantApplication([FromRoute] Guid applicationId, CancellationToken cancellationToken)
        {
            Guid dummyUserId = Guid.NewGuid();

            var result = await _mediator.Send(new ApproveRestaurantApplicationCommand(dummyUserId, applicationId));

            return CreatedAtAction(nameof(GetById),new { id = result, version = "1.0" },new { id = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            await Task.Delay(19);
            return Ok(new { });
        }
    }
}