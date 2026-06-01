using MediatR;

namespace Restaurants.Application.UseCases.Commands.UpdateRestaurantBanner
{
    public sealed record UpdateRestaurantBannerCommand(Guid RestaurantId, string BannerUrl) : IRequest;
}
