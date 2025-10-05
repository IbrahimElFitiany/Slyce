using Restaurants.Application.Interfaces;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;
namespace Restaurants.Application.UseCases
{
    public class RegisterRestaurantUseCase
    {
        private readonly IRestaurantRepository _repository;
        public RegisterRestaurantUseCase(IRestaurantRepository restaurantRepository)
        {
            _repository = restaurantRepository;
        }

        public async Task<RegisterRestaurantResponse> ExecuteAsync(RegisterRestaurantRequest restaurantRequest,CancellationToken cancellationToken)
        {
            var restaurant = new Restaurant(restaurantRequest.Name, restaurantRequest.Description, restaurantRequest.Type);
            await _repository.AddAsync(restaurant, cancellationToken);
            return new RegisterRestaurantResponse(restaurant.Id , restaurant.Name, restaurant.Description, restaurant.Type.ToString(),restaurant.Status.ToString());
        }

    }
}
