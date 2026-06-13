using MediatR;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.ValueObjects;
using Shared.Application.Exceptions;

namespace Restaurants.Application.UseCases.Commands.UpdateBranchSchedule
{
    internal sealed class UpdateBranchScheduleCommandHandler(
        IRestaurantBranchRepository branchRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateBranchScheduleCommand>
    {
        public async Task Handle(UpdateBranchScheduleCommand command, CancellationToken cancellationToken)
        {
            var branch = await branchRepository.GetByIdAsync(command.BranchId, cancellationToken)
                ?? throw new NotFoundException("Branch", command.BranchId);

            if (branch.RestaurantId != command.RestaurantId)
                throw new NotFoundException("Branch", command.BranchId);

            var schedule = command.Schedule
                .Select(s => new DailySchedule(s.Day, new OperatingHours(s.OpenTime, s.CloseTime)))
                .ToList();

            branch.UpdateSchedule(schedule);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}