using MediatR;
using Menus.Domain.Entities;
using Menus.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Food.Contracts;
using Food.Contracts.DTOs;
using Menus.Domain.ValueObjects;
using Shared.Domain.ValueObjects;
using Shared.Application.Exceptions;
using Menus.Domain.Services;
using Shared.Domain.Exceptions;
using Menus.Domain;
using Menus.Application.Mappers;

namespace Menus.Application.UseCases.Commands.CreateMenuMeal
{
    internal sealed class CreateMenuMealCommandHandler (
        IUnitOfWork unitOfWork,
        IFoodQueryServices foodService,
        IMenuMealRepository mealRepository,
        IMenuCategoryRepository categoryRepository,
        ILogger<CreateMenuMealCommandHandler> logger) : IRequestHandler<CreateMenuMealCommand,Guid>
    {
        public async Task<Guid> Handle(CreateMenuMealCommand command, CancellationToken ct)
        {
            //TODO check user permissions

            var category = await categoryRepository.GetByIdAsync(command.CategoryId, ct);

            if (category is null || category.RestaurantId != command.RestaurantId)
                throw new NotFoundException("Category", command.CategoryId);
            
            await EnsureUniqueMealName(command.Name, command.RestaurantId, ct);

            var nutritionByIngredient = await foodService.GetFoodNutritionsAsync(command.Ingredients, ct);

            ValidateAllIngredientsFound(command.Ingredients.ToList(), nutritionByIngredient);

            var mealIngredients = BuildMealIngredients(nutritionByIngredient);
            var nutritionMap = NutritionMapper.ToNutrition(nutritionByIngredient);
            var sizes = BuildMealSizes(command.Sizes, nutritionMap);

            var meal = new MenuMeal(
                command.CategoryId,
                command.RestaurantId,
                command.Name,
                command.Description,
                command.ImgUrl,
                mealIngredients,
                sizes);

            mealRepository.Add(meal);
            await unitOfWork.SaveChangesAsync(ct);

            logger.LogInformation("Meal created successfully. MealId={MealId}, Name={Name}, RestaurantId={RestaurantId}", meal.Id, meal.Name, meal.RestaurantId);

            return meal.Id;
        }

        private async Task EnsureUniqueMealName(string mealName, Guid restaurantId, CancellationToken ct)
        {
            if (await mealRepository.ExistsByNameInRestaurantAsync(mealName, restaurantId, ct))
                throw new DuplicateException("Meal",mealName);
        }
        private void ValidateAllIngredientsFound(IReadOnlyList<Guid> requestFoodIds, Dictionary<Guid,FoodNutritionDTO> ingredientsRetrived)
        {
            var missingIngredients = requestFoodIds.Except(ingredientsRetrived.Keys);

            if (missingIngredients.Any())
            {
                logger.LogWarning("Ingredients missing in food service. Missing ={list}", missingIngredients);
                throw new NotFoundException("ingredients not found");
            }
        }
        private List<MealIngredient> BuildMealIngredients(Dictionary<Guid, FoodNutritionDTO> map)
        {
            return map.Select(ig => new MealIngredient(ig.Key, ig.Value.Name)).ToList();
        }
        private List<MealSizeCreationInput> BuildMealSizes(IEnumerable<MealSizeInput> mealSizeInputs, Dictionary<Guid, Nutrition> nutritionMap)
        {
            return mealSizeInputs.Select(mealSize =>
            {
                var quantities = mealSize.IngredientQuantities
                    .Select(iq => new IngredientQuantity(iq.IngredientId, iq.Quantity))
                    .ToList();

                var nutrition = NutritionCalculator.CalculateForSize(quantities, nutritionMap);

                return new MealSizeCreationInput(
                    Name: mealSize.Name,
                    Price: Price.EGP(mealSize.Price),
                    SortOrder: mealSize.SortOrder,
                    Quantities: quantities,
                    SizeNutrition: nutrition
                    );
            }).ToList();
        }

    }
}