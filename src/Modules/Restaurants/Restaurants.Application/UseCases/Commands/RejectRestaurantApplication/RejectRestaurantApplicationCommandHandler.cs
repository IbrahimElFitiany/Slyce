using MediatR;
using Restaurants.Application.Interfaces;
using Shared.Application.Exceptions;

namespace Restaurants.Application.UseCases.Commands.RejectRestaurantApplication
{
    internal sealed class RejectRestaurantApplicationCommandHandler(
        IRestaurantApplicationRepository restaurantApplicationRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<RejectRestaurantApplicationCommand>
    {
        public async Task Handle(RejectRestaurantApplicationCommand command, CancellationToken ct)
        {
            var restaurantApplication = await restaurantApplicationRepository.GetByIdAsync(command.ApplicationId, ct)
                ?? throw new NotFoundException("restaurant-application", command.ApplicationId);

            restaurantApplication.Reject(command.UserId, command.RejectReason);

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
