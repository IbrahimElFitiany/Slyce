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

namespace Menus.Application.UseCases.Commands.CreateMenuMeal
{
    public class CreateMenuMealCommandHandler : IRequestHandler<CreateMenuMealCommand,Guid>
    {
        private readonly IMenuMealRepository _mealRepository;
        private readonly IFoodServices _foodService;
        private readonly ILogger<CreateMenuMealCommandHandler> _logger;

        public CreateMenuMealCommandHandler(
            IMenuMealRepository repository,
            IFoodServices foodService,
            ILogger<CreateMenuMealCommandHandler> logger)
        {
            _mealRepository = repository;
            _foodService = foodService;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateMenuMealCommand request, CancellationToken ct)
        {
            //TODO check user permissions

            await EnsureUniqueMealName(request.Name, request.RestaurantId, ct);

            var nutritionByIngredient = await _foodService.GetFoodNutritionsAsync(request.Ingredients.ToList(), ct);

            ValidateAllIngredientsFound(request.Ingredients.ToList(), nutritionByIngredient);

            var mealIngredients = BuildMealIngredients(nutritionByIngredient);
            var nutritionMap = BuildNutritionMap(nutritionByIngredient);
            var sizes = BuildMealSizes(request.Sizes, nutritionMap);

            var meal = new MenuMeal(
                request.CategoryId,
                request.RestaurantId,
                request.Name,
                request.Description,
                request.ImgUrl,
                mealIngredients,
                sizes
            );

            await _mealRepository.AddAsync(meal, ct);

            _logger.LogInformation("Meal created successfully. MealId={MealId}, Name={Name}, RestaurantId={RestaurantId}", meal.Id, meal.Name, meal.RestaurantId);
            return meal.Id;
        }

        private async Task EnsureUniqueMealName(string mealName, Guid restaurantId, CancellationToken ct)
        {
            if (await _mealRepository.ExistsByNameInRestaurantAsync(mealName, restaurantId, ct))
                throw new DuplicateException("Meal",mealName);
        }
        private void ValidateAllIngredientsFound(IReadOnlyList<Guid> requestIds, Dictionary<Guid,FoodNutritionDTO> ingredientsRetrived)
        {
            var missingIngredients = requestIds.Except(ingredientsRetrived.Keys);

            if (missingIngredients.Any())
            {
                _logger.LogWarning("Ingredients missing in food service. Missing ={list}", missingIngredients);
                throw new NotFoundException("ingredients not found");
            }
        }
        private Dictionary<Guid, Nutrition> BuildNutritionMap(Dictionary<Guid, FoodNutritionDTO> nutritionData)
        {
            return nutritionData.ToDictionary(
                kvp => kvp.Key,
                kvp => new Nutrition(
                    protein: kvp.Value.Protein,
                    carb: kvp.Value.TotalCarbohydrate,
                    fat: kvp.Value.TotalFat,
                    calories: kvp.Value.Calories,
                    saturatedFat: kvp.Value.SaturatedFat,
                    transFat: kvp.Value.TransFat,
                    cholesterol: kvp.Value.Cholesterol,
                    sodiumMg: kvp.Value.SodiumMg,
                    dietaryFiber: kvp.Value.DietaryFiber,
                    sugarGrams: kvp.Value.SugarGrams,
                    vitaminD: kvp.Value.VitaminD,
                    calciumMg: kvp.Value.CalciumMg,
                    ironMg: kvp.Value.IronMg,
                    potassiumMg: kvp.Value.PotassiumMg,
                    vitaminA_Mcg: kvp.Value.VitaminAMcg,
                    vitaminC_Mg: kvp.Value.VitaminCMg
                )
            );
        }
        private List<MealIngredient> BuildMealIngredients(Dictionary<Guid, FoodNutritionDTO> map)
        {
            return map.Select(ig => new MealIngredient(ig.Key, ig.Value.Name)).ToList();
        }
        private List<MealSize> BuildMealSizes(IEnumerable<MealSizeInput> mealSizeInputs, Dictionary<Guid, Nutrition> nutritionMap)
        {
            return mealSizeInputs.Select(mealSize =>
            {
                var quantities = mealSize.IngredientQuantities
                    .Select(iq => new IngredientQuantity(iq.IngredientId, iq.Quantity))
                    .ToList();

                var nutrition = NutritionCalculator.CalculateForSize(quantities, nutritionMap);

                return new MealSize(
                    mealSize.Name,
                    Price.EGP(mealSize.Price),
                    mealSize.SortOrder,
                    quantities,
                    nutrition
                );
            }).ToList();
        }

    }
}