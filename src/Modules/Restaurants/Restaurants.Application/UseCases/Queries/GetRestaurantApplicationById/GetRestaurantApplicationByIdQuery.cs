using MediatR;

namespace Restaurants.Application.UseCases.Queries.GetRestaurantApplicationById
{
    public sealed record GetRestaurantApplicationByIdQuery(Guid RestaurantApplicationId) : IRequest<GetRestaurantApplicationByIdResult>;

    public sealed record GetRestaurantApplicationByIdResult(
        Guid Id,
        string BrandName,
        string OwnerFirstName,
        string OwnerLastName,
        string OwnerEmail,
        string OwnerMobileNumber,
        string CompanyEmail,
        string CompanyMobileNumber,
        string RestaurantType,
        int BranchCount,
        string? Description,
        string Status,
        string? RejectionReason,
        DateTime SubmittedAt,
        DateTime? ReviewedAt,
        string? ReviewedBy,
        string? StreetName,
        string? StreetNumber,
        string Area,
        string City,
        double Latitude,
        double Longitude);
}