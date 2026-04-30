using MediatR;
using Food.Contracts;
using Menus.Application.Interfaces;
using Menus.Application.Mappers;
using Menus.Domain.Entities;
using Menus.Domain.Exceptions;
using Menus.Domain.Services;
using Menus.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;

namespace Menus.Application.UseCases.Commands.AddMealSize
{
    public sealed class AddMealSizeCommandHandler : IRequestHandler<AddMealSizeCommand, Guid>
    {
        private readonly IMenuMealRepository _menuMealRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFoodServices _foodServices;
        private readonly ILogger<AddMealSizeCommandHandler> _logger;

        public AddMealSizeCommandHandler(
            IMenuMealRepository menuMealRepository,
            IUnitOfWork unitOfWork,
            IFoodServices foodServices,
            ILogger<AddMealSizeCommandHandler> logger)
        {
            _menuMealRepository = menuMealRepository;
            _unitOfWork = unitOfWork;
            _foodServices = foodServices;
            _logger = logger;
        }

        public async Task<Guid> Handle(AddMealSizeCommand request, CancellationToken ct)
        {
            var meal = await _menuMealRepository.GetByIdAsync(request.MealId, ct) ??
                 throw new NotFoundException(nameof(MenuMeal), request.MealId);

            var sizeIngredientFoodIds = request.IngredientQuantities.Select(iq => iq.IngredientId).ToList();

            if (!meal.HasExactIngredients(sizeIngredientFoodIds))
                throw new MealSizeIngredientMismatchException(request.Name);

            var ingredientsNutrition = await _foodServices.GetFoodNutritionsAsync(sizeIngredientFoodIds, ct);

            var sizeIngredientQuantities = request.IngredientQuantities.Select(iq => new IngredientQuantity(iq.IngredientId, iq.Quantity)).ToList();

            var sizeNutrition = NutritionCalculator.CalculateForSize(
                ingredientQuantities: sizeIngredientQuantities,
                nutritionByIngredient: NutritionMapper.ToNutrition(ingredientsNutrition));

            var sizeId = meal.AddSize(
                name: request.Name,
                price: Price.EGP(request.Price),
                sortOrder: request.SortOrder,
                ingredientQuantities: sizeIngredientQuantities,
                sizeNutrition: sizeNutrition);
            
            await _unitOfWork.SaveChangesAsync(ct);
            _logger.LogInformation("Size {MealSizeId} added to meal {MealId}", sizeId, meal.Id);

            return sizeId;
        }
    }
}