using MediatR;

namespace Restaurants.Application.UseCases.Commands.UpdateRestaurantImage
{
    public sealed record UpdateRestaurantImageCommand(Guid RestauarntId, string ImageUrl) : IRequest;
}