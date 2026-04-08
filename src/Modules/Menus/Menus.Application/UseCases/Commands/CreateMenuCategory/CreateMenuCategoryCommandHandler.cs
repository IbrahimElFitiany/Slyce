using MediatR;
using Menus.Domain.Entities;
using Menus.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Restaurants.Contracts.Interfaces;


namespace Menus.Application.UseCases.Commands.CreateMenuCategory
{
    public class CreateMenuCategoryCommandHandler : IRequestHandler<CreateMenuCategoryCommand,Guid>
    {
        private readonly IMenuCategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestaurantServices _restaurantServices;
        private readonly ILogger<CreateMenuCategoryCommandHandler> _logger;

        public CreateMenuCategoryCommandHandler(
            IMenuCategoryRepository repository,
            IRestaurantServices restaurantServices,
            ILogger<CreateMenuCategoryCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _restaurantServices = restaurantServices;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateMenuCategoryCommand request, CancellationToken cancellationToken)
        {

            if (!await _restaurantServices.ExistsAsync(request.RestaurantId,cancellationToken)) 
                throw new Exception("restaurant is not there");

            var newCategory = new MenuCategory(request.RestaurantId, request.Name);

            _repository.Add(newCategory);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Resataurant {RestaurantId} create a new Category {CategoryId}", newCategory.RestaurantId , newCategory.Id);

            return newCategory.Id;
        }
    }
}