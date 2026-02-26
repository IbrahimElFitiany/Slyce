using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Domain.ValueObjects;
using Shared.Application.Exceptions;

namespace Restaurants.Application.UseCases.Commands.CreateBranchSchedule
{
    public sealed class CreateBranchScheduleCommandHandler : IRequestHandler<CreateBranchScheduleCommand>
    {
        private readonly ILogger<CreateBranchScheduleCommandHandler> _logger;
        private readonly IRestaurantBranchRepository _branchRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBranchScheduleCommandHandler (
            ILogger<CreateBranchScheduleCommandHandler> logger,
            IRestaurantBranchRepository branchRepository,
            IUnitOfWork unitOfWork) 
        {
            _logger = logger;
            _branchRepository = branchRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateBranchScheduleCommand request, CancellationToken ct)
        {
            var branch = await _branchRepository.GetByIdAsync(request.BranchId, ct);

            if (branch is null)
                throw new NotFoundException(nameof(RestaurantBranch), request.BranchId);

            branch.InitializeSchedule(
                request.Schedule
                .Select(d => new DailySchedule(d.Day,new OperatingHours(d.OpeningTime, d.ClosingTime))).ToList());

            await _unitOfWork.SaveChangesAsync(ct);

            _logger.LogInformation("Added working hours to branch {BranchId}", request.BranchId);
        }

    }
}