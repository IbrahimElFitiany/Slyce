using MediatR;
using Restaurants.Application.Interfaces;
using Shared.Application.Exceptions;

namespace Restaurants.Application.UseCases.Commands.ActivateRestaurant
{
    internal sealed class ActivateRestaurantCommandHandler(
        IRestaurantRepository restaurantRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<ActivateRestaurantCommand>
    {
        public async Task Handle(ActivateRestaurantCommand command, CancellationToken ct)
        {
            var restaurant = await restaurantRepository.GetByIdAsync(command.RestaurantId, ct)
                ?? throw new NotFoundException("Restaruant", command.RestaurantId);

            restaurant.Activate();

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
