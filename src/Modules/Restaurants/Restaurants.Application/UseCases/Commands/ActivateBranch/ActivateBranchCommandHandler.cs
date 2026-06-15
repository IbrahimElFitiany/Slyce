using MediatR;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Restaurants.Application.UseCases.Commands.ActivateBranch
{
    internal sealed class ActivateBranchCommandHandler(
        IUnitOfWork unitOfWork,
        IRestaurantBranchRepository restaurantBranchRepository) : IRequestHandler<ActivateBranchCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IRestaurantBranchRepository _restaurantBranchRepository = restaurantBranchRepository;
        
        public async Task Handle(ActivateBranchCommand command, CancellationToken ct)
        {
            var branch = await _restaurantBranchRepository.GetByIdAsync(command.BranchId, ct)
                ?? throw new NotFoundException("Branch", command.BranchId);

            branch.Activate();
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}