using MediatR;
using Menus.Domain.Entities;
using Menus.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Restaurants.Contracts;


namespace Menus.Application.UseCases.Commands.CreateMenuCategory
{
    public class CreateMenuCategoryCommandHandler : IRequestHandler<CreateMenuCategoryCommand>
    {
        private readonly IMenuCategoryRepository _repository;
        private readonly IRestaurantServices _restaurantServices;
        private readonly ILogger<CreateMenuCategoryCommandHandler> _logger;

        public CreateMenuCategoryCommandHandler(
            IMenuCategoryRepository repository,
            IRestaurantServices restaurantServices,
            ILogger<CreateMenuCategoryCommandHandler> logger)
        {
            _repository = repository;
            _restaurantServices = restaurantServices;
            _logger = logger;
        }

        public async Task Handle(CreateMenuCategoryCommand request, CancellationToken cancellationToken)
        {
            var dto = request.dto;

            if (!await _restaurantServices.ExistsAsync(dto.restaurantId)) {

                throw new Exception("restaurant is not there");
            }

            MenuCategory newCategory = new MenuCategory(dto.restaurantId, dto.name);

            await _repository.AddAsync(newCategory, cancellationToken);

            _logger.LogInformation("Resataurant {RestaurantId} create a new Category {CategoryId}", newCategory.RestaurantId , newCategory.Id);
        }
    }
}