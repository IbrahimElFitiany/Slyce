using MediatR;

namespace Restaurants.Application.UseCases.Commands.ActivateRestaurant
{
    public sealed record ActivateRestaurantCommand(Guid RestaurantId) : IRequest;
}
