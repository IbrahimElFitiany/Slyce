using Food.Application.Interfaces;
using FoodEntity = Food.Domain.Entities.Food;
using MediatR;
using Shared.Domain.ValueObjects;
using Food.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace Food.Application.UseCases.Commands.ImportExternalFood
{
    internal sealed class ImportExternalFoodsCommandHandler(
        IFoodRepository foodRepository,
        IExternalFoodService externalFoodService,
        ILogger<ImportExternalFoodsCommandHandler> logger) : IRequestHandler<ImportExternalFoodsCommand, IEnumerable<FoodSummaryDTO>>
    {

        public async Task<IEnumerable<FoodSummaryDTO>> Handle(ImportExternalFoodsCommand request, CancellationToken cancellationToken)
        {
            var externalFoods = await externalFoodService.SearchAsync(request.SearchTerm, cancellationToken);

            var foods = ExternalFoodToFoodEntities(externalFoods);

            await foodRepository.UpsertRange(foods, cancellationToken);

            var newFood = await foodRepository.GetByExternalId(foods.Select(f => f.ExternalId), cancellationToken);

            logger.LogInformation("added external food to db");

            return newFood.Select(f => new FoodSummaryDTO(
                Id: f.Id,
                Name: f.Name,
                ImageUrl: f.Image,
                Calories: f.NutritionPer100g.Calories,
                Protien: f.NutritionPer100g.Protein,
                Carbs: f.NutritionPer100g.TotalCarbohydrate,
                Fat: f.NutritionPer100g.TotalFat
            ));
        }

        private static IEnumerable<FoodEntity> ExternalFoodToFoodEntities(IEnumerable<ExternalFoodResultDTO> externalFoods)
        {
            return externalFoods.Select(externalFood => new FoodEntity(
                name: externalFood.Name,
                imageUrl: externalFood.ImageUrl,
                nutrition: new Nutrition(
                    protein: externalFood.Protein,
                    carb: externalFood.Carbs,
                    fat: externalFood.Fat,
                    calories: externalFood.Calories,
                    saturatedFat: externalFood.SaturatedFat,
                    transFat: externalFood.TransFat,
                    cholesterol: externalFood.Cholesterol,
                    sodiumMg: externalFood.SodiumMg,
                    dietaryFiber: externalFood.DietaryFiber,
                    sugarGrams: externalFood.SugarGrams,
                    vitaminD: externalFood.VitaminD,
                    calciumMg: externalFood.CalciumMg,
                    ironMg: externalFood.IronMg,
                    potassiumMg: externalFood.PotassiumMg,
                    vitaminA_Mcg: externalFood.VitaminAMcg,
                    vitaminC_Mg: externalFood.VitaminCMg
                ),
                externalId: externalFood.ExternalId,
                source: externalFood.Source
            ));
        }
    }
}