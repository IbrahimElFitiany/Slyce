using Food.Application.Interfaces;
using FoodEntity = Food.Domain.Entities.Food;
using MediatR;
using Shared.Domain.ValueObjects;
using Food.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace Food.Application.UseCases.Commands.ImportExternalFood
{
    public sealed class ImportExternalFoodsCommandHandler : IRequestHandler<ImportExternalFoodsCommand, IEnumerable<FoodSummaryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFoodRepository _foodRepository;
        private readonly IExternalFoodService _externalFoodService;
        private readonly ILogger<ImportExternalFoodsCommandHandler> _logger;

        public ImportExternalFoodsCommandHandler(
            IUnitOfWork unitOfWork,
            IFoodRepository foodRepository,
            IExternalFoodService externalFoodService,
            ILogger<ImportExternalFoodsCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _foodRepository = foodRepository;
            _externalFoodService = externalFoodService;
            _logger = logger;
        }

        public async Task<IEnumerable<FoodSummaryDTO>> Handle(ImportExternalFoodsCommand request, CancellationToken cancellationToken)
        {
            var externalFoods = await _externalFoodService.SearchAsync(request.SearchTerm, cancellationToken);

            var foods = ExternalFoodToFoodEntities(externalFoods);

            await _foodRepository.InsertIfNotExistsAsync(foods, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("added external food to db");
            return foods.Select(f => new FoodSummaryDTO(
                Id: f.Id,
                Name: f.Name,
                ImageUrl: f.Image,
                Calories: f.NutritionPer100g.Calories,
                Protien: f.NutritionPer100g.Protein,
                Carbs: f.NutritionPer100g.TotalCarbohydrate,
                Fat: f.NutritionPer100g.TotalFat
            ));
        }

        private IEnumerable<FoodEntity> ExternalFoodToFoodEntities(IEnumerable<ExternalFoodResultDTO> externalFoods)
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