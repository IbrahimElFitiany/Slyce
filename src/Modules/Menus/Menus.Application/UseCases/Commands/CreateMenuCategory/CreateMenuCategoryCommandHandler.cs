using MediatR;
using Menus.Domain.Entities;
using Menus.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Restaurants.Contracts.Interfaces;
using Shared.Application.Exceptions;
using Menus.Domain.Repositories;


namespace Menus.Application.UseCases.Commands.CreateMenuCategory
{
    internal sealed class CreateMenuCategoryCommandHandler(
        IMenuCategoryRepository menuCategoryRepository,
        IRestaurantQueryServices restaurantQueryServices,
        ILogger<CreateMenuCategoryCommandHandler> logger,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateMenuCategoryCommand, Guid>
    {
        public async Task<Guid> Handle(CreateMenuCategoryCommand command, CancellationToken ct)
        {

            if (!await restaurantQueryServices.ExistsAsync(command.RestaurantId, ct)) 
                throw new NotFoundException("restaurant", command.RestaurantId);

            var newCategory = new MenuCategory(command.RestaurantId, command.Name);

            menuCategoryRepository.Add(newCategory);

            await unitOfWork.SaveChangesAsync(ct);

            logger.LogInformation("Resataurant {RestaurantId} create a new Category {CategoryId}", newCategory.RestaurantId , newCategory.Id);

            return newCategory.Id;
        }
    }
}