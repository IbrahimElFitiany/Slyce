using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurants.Application.UseCases.Queries.GetRestaurantApplicationById;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Queries
{
    internal sealed class GetRestaurantApplicationByIdQueryHandler(
        RestaurantDbContext restaurantDbContext) : IRequestHandler<GetRestaurantApplicationByIdQuery, GetRestaurantApplicationByIdResult>
    {
        public async Task<GetRestaurantApplicationByIdResult?> Handle(GetRestaurantApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            var application = await restaurantDbContext.RestaurantApplications
                .AsNoTracking()
                .Where(ra => ra.Id == request.RestaurantApplicationId)
                .Select(ra => new GetRestaurantApplicationByIdResult(
                    ra.Id,
                    ra.BrandName,
                    ra.OwnerFirstName,
                    ra.OwnerLastName,
                    ra.OwnerEmail.Value,
                    ra.OwnerMobileNumber.Value,
                    ra.CompanyEmail.Value,
                    ra.CompanyMobileNumber.Value,
                    ra.RestaurantType.ToString(),
                    ra.BranchCount,
                    ra.Description,
                    ra.Status.ToString(),
                    ra.RejectionReason,
                    ra.SubmittedAt,
                    ra.ReviewedAt,
                    ra.ReviewedBy.ToString(),
                    ra.MainBranchLocation.StreetName,
                    ra.MainBranchLocation.StreetNumber,
                    ra.MainBranchLocation.Area,
                    ra.MainBranchLocation.City,
                    ra.MainBranchLocation.Coordinates.Latitude,
                    ra.MainBranchLocation.Coordinates.Longitude))
                .FirstOrDefaultAsync(cancellationToken);

            return application;
        }
    }
}
