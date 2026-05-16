using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.UseCases.Commands.ApproveRestaurantApplication;
using Restaurants.Application.UseCases.Commands.CreateRestaurantApplication;
using Restaurants.Application.UseCases.Commands.RejectRestaurantApplication;
using Restaurants.Application.UseCases.Queries.GetRestaurantApplicationsSummary;
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
        public async Task<IActionResult> CreateRestaurantApplication(
            [FromBody] CreateRestaurantApplicationRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateRestaurantApplicationCommand(
                BrandName: request.BrandName,
                OwnerFirstName: request.OwnerFirstName,
                OwnerLastName: request.OwnerLastName,
                CompanyEmail: request.CompanyEmail,
                OwnerEmail: request.OwnerEmail,
                OwnerMobileNumber: request.OwnerMobileNumber,
                CompanyMobileNumber: request.CompanyMobileNumber,
                RestaurantType: request.RestaurantType,
                MainBranchAddress: new MainBranchAddressInput(
                    City: request.MainBranchAddress.City,
                    Area: request.MainBranchAddress.Area,
                    StreetName: request.MainBranchAddress.StreetName,
                    StreetNumber: request.MainBranchAddress.StreetNumber,
                    Latitude: request.MainBranchAddress.Latitude,
                    Longitude: request.MainBranchAddress.Longitude
                ),
                BranchCount: request.BranchCount,
                Description: request.Description);

            var result = await mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = result }, new { applicationId = result });
        }

        [Authorize (Roles = "Admin")]
        [HttpPost("{applicationId}/approve")]
        public async Task<IActionResult> ApproveRestaurantApplication([FromRoute] Guid applicationId, CancellationToken ct)
        { 
            await mediator.Send(new ApproveRestaurantApplicationCommand(UserId, applicationId), ct);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{applicationId}/reject")]
        public async Task<IActionResult> RejectRestaurantApplication(
            [FromRoute] Guid applicationId,
            [FromBody] RejectRestaurantApplicationRequest request,
            CancellationToken ct)
        {
            await mediator.Send(new RejectRestaurantApplicationCommand(applicationId, UserId, request.RejectionReason), ct);

            return NoContent();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetRestaurantApplicationsSummary(
            [FromQuery] GetRestaurantApplicationsSummaryRequest request,
            CancellationToken ct)
        {
            var query = new GetRestaurantApplicationsSummaryQuery(
                request.Status,
                request.SortDescending,
                request.From,
                request.To,
                request.Page,
                request.PageSize);

            var applications = await mediator.Send(query, ct);

            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            await Task.Delay(19);
            return Ok(new { });
        }
    }
}