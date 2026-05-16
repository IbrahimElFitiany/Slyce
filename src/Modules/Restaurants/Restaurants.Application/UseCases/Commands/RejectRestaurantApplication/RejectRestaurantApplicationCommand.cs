using MediatR;

namespace Restaurants.Application.UseCases.Commands.RejectRestaurantApplication
{
    public sealed record RejectRestaurantApplicationCommand(
        Guid ApplicationId,
        Guid UserId,
        string? RejectReason) : IRequest;
}
