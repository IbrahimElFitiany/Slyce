using MediatR;
using Shared.Application;

namespace Restaurants.Application.UseCases.Queries.GetPendingBranches
{
    public sealed record GetPendingBranchesQuery(int Page, int PageSize) : IRequest<PagedResult<PendingBranchSummary>>;

    public sealed record PendingBranchSummary(
        Guid BranchId,
        Guid RestaurantId,
        string RestaurantName,
        string RestaurantLogoUrl,
        string RestaurantBannerUrl,
        string BranchName,
        string City,
        string Area,
        double Lat,
        double Lng,
        DateTime CreatedAt);
}