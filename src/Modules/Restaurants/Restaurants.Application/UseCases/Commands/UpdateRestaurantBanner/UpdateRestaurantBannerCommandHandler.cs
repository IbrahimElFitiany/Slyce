using MediatR;
using Restaurants.Application.Interfaces;
using Shared.Application.Exceptions;

namespace Restaurants.Application.UseCases.Commands.UpdateRestaurantBanner
{
    internal sealed class UpdateRestaurantBannerCommandHandler(
        IRestaurantRepository restaurantRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateRestaurantBannerCommand>
    {
        public async Task Handle(UpdateRestaurantBannerCommand command, CancellationToken ct)
        {
            var restaurant = await restaurantRepository.GetByIdAsync(command.RestaurantId, ct)
                ?? throw new NotFoundException("restaurant", command.RestaurantId);

            restaurant.UpdateBanner(command.BannerUrl);

            await unitOfWork.SaveChangesAsync(ct);    
        }
    }
}