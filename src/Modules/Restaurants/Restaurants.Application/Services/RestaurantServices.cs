using Restaurants.Application.Interfaces;
using Restaurants.Contracts.DTOs;
using Restaurants.Contracts.Interfaces;
using Restaurants.Domain.Entities;
using Shared.Application.Exceptions;

namespace Restaurants.Application.Services
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantBranchRepository _restaurantBranchRepository;

        public RestaurantServices(
            IRestaurantRepository restaurantRepository,
            IRestaurantBranchRepository restaurantBranchRepository)
        {
            _restaurantRepository = restaurantRepository;
            _restaurantBranchRepository = restaurantBranchRepository;
        }

        public async Task<bool> ExistsAsync(Guid restaurantId, CancellationToken ct)
        {
            return await _restaurantRepository.ExistsAsync(restaurantId, ct);
        }

        public async Task<BranchForSubscription> GetBranchForSubscriptionAsync(Guid branchId, CancellationToken ct)
        {
            var branch = await _restaurantBranchRepository.GetByIdAsync(branchId, ct);
            if (branch is null)
                throw new NotFoundException(nameof(RestaurantBranch), branchId);

            return new BranchForSubscription(
                RestaurantId: branch.RestaurantId,
                Longitude: branch.Address.Coordinates.Longitude,
                Latitude: branch.Address.Coordinates.Latitude,
                Schedule: branch.Schedule.ToDictionary(
                    s => s.Day,
                    s => new TimeFrame(s.OperatingHours.OpeningTime, s.OperatingHours.ClosingTime)
                ));
        }
    }
}
