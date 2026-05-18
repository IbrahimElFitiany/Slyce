using MediatR;
using Restaurants.Application.Interfaces;
using Shared.Application.Exceptions;

namespace Restaurants.Application.UseCases.Commands.UpdateRestaurantImage
{
    internal sealed class UpdateRestaurantImageCommandHandler(
        IRestaurantRepository restaurantRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateRestaurantImageCommand>
    {
        public async Task Handle(UpdateRestaurantImageCommand command, CancellationToken ct)
        {
            var restaurant = await restaurantRepository.GetByIdAsync(command.RestauarntId,ct)
                ?? throw new NotFoundException("Restuanant", command.RestauarntId);

            restaurant.UpdateLogo(command.ImageUrl);

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
