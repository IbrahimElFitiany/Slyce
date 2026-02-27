using MediatR;

namespace Restaurants.Application.UseCases.Commands.ApproveRestaurantApplication
{
    public sealed record ApproveRestaurantApplicationCommand(Guid UserId, Guid ApplicationId):IRequest<Guid>;
}
