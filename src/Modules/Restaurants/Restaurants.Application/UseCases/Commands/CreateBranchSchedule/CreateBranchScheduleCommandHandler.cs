using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.ValueObjects;
using Shared.Application.Exceptions;

namespace Restaurants.Application.UseCases.Commands.CreateBranchSchedule
{
    internal sealed class CreateBranchScheduleCommandHandler(
        ILogger<CreateBranchScheduleCommandHandler> logger,
        IRestaurantBranchRepository branchRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateBranchScheduleCommand>
    {
        
        public async Task Handle(CreateBranchScheduleCommand command, CancellationToken ct)
        {

            var branch = await branchRepository.GetByIdAsync(command.BranchId, ct)
                ?? throw new NotFoundException(nameof(RestaurantBranch), command.BranchId);

            branch.InitializeSchedule(
                command.Schedule
                .Select(d => new DailySchedule(d.Day,new OperatingHours(d.OpeningTime, d.ClosingTime))).ToList());

            await unitOfWork.SaveChangesAsync(ct);

            logger.LogInformation("Added working hours to branch {BranchId}", command.BranchId);
        }

    }
}