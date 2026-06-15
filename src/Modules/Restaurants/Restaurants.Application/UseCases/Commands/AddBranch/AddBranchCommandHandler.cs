using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;


namespace Restaurants.Application.UseCases.Commands.AddBranch
{
    public sealed class AddBranchCommandHandler (
        IUnitOfWork unitOfWork,
        ILogger<AddBranchCommandHandler> logger,
        IRestaurantRepository restaurantRepository,
        IRestaurantBranchRepository branchRepository) : IRequestHandler<AddBranchCommand,Guid>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<AddBranchCommandHandler> _logger = logger;
        private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
        private readonly IRestaurantBranchRepository _branchRepository = branchRepository;

        public async Task<Guid> Handle(AddBranchCommand command, CancellationToken ct)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(command.RestaurantId, ct)
                ?? throw new NotFoundException("Restaurant", command.RestaurantId);

            var branch = new RestaurantBranch(
                command.RestaurantId,
                command.BranchName,
                new Address(
                    command.Address.City,
                    command.Address.Area,
                    command.Address.StreetName,
                    command.Address.StreetNumber,
                    new Coordinates(
                        command.Address.Latitude,
                        command.Address.Longitude)),
                PhoneNumber.Create(command.BranchContactNumber));

            _branchRepository.Add(branch);
            await _unitOfWork.SaveChangesAsync(ct);

            return branch.Id;
        }
    }
}