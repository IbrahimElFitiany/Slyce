using FluentAssertions;
using Food.Contracts;
using Food.Contracts.DTOs;
using Menus.Application.Interfaces;
using Menus.Application.UseCases.Commands.AddMealSize;
using Menus.Domain;
using Menus.Domain.Entities;
using Menus.Domain.Exceptions;
using Menus.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;
using System.Xml.Serialization;

namespace Menus.Application.Tests.Commands
{
    public class AddMealSizeCommandHandlerTests
    {
        private readonly Mock<IMenuMealRepository> _menuMealRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ILogger<AddMealSizeCommandHandler> _logger;
        private readonly Mock<IFoodServices> _foodServiceMock;
        private readonly AddMealSizeCommandHandler _handler;

        public AddMealSizeCommandHandlerTests()
        {
            _menuMealRepositoryMock = new();
            _unitOfWorkMock = new();
            _logger = NullLogger<AddMealSizeCommandHandler>.Instance;
            _foodServiceMock = new();

            _handler = new AddMealSizeCommandHandler(
                _menuMealRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _foodServiceMock.Object,
               _logger);
        }

        [Fact]
        public async Task Handle_Should_Throw_WhenMealNotFound()
        {
            // arrange
            var command = new AddMealSizeCommand(
                MealId: It.IsAny<Guid>(),
                Name: It.IsAny<string>(),
                Price: 9999m,
                SortOrder: 4,
                IngredientQuantities: new List<IngredientQuantityInput>
                {
                    new (Guid.NewGuid(),123m),
                    new (Guid.NewGuid(),123m)
                });

            _menuMealRepositoryMock
                .Setup(repo => repo.GetByIdAsync(command.MealId, default))
                .ReturnsAsync((MenuMeal?)null);

            //act
            var act = async () => await _handler.Handle(command,It.IsAny<CancellationToken>());

            //assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_Should_Throw_WhenSizeIngredientsMismatchBaseIngredients()
        {
            // Arrange
            var command = new AddMealSizeCommand(
                MealId: Guid.NewGuid(),
                Name: "MealName",
                Price: 242.2m,
                SortOrder: 2,
                IngredientQuantities: new List<IngredientQuantityInput>
                {
                    new (Guid.NewGuid(),123m),
                    new (Guid.NewGuid(),123m)
                });

            _menuMealRepositoryMock
                .Setup(repo => repo.GetByIdAsync(command.MealId, default))
                .ReturnsAsync(CreateValidMeal());

            // act

            var act = async () => await _handler.Handle(command,It.IsAny<CancellationToken>());

            // assert
            await act.Should().ThrowAsync<MealSizeIngredientMismatchException>();
        }

        [Fact]
        public async Task Handle_Should_ReturnSizeId_When_AllValidAsync()
        {
            //arrange            
            var ingredient1 = Guid.NewGuid();
            var ingredient2 = Guid.NewGuid();

            var baseIngredients = new List<MealIngredient> {
                new (ingredient1, "ingredient-1"),
                new (ingredient2, "ingredient-w")
            };

            var ingredientQts = new List<IngredientQuantity> {
                new IngredientQuantity(ingredient1,123m),
                new IngredientQuantity(ingredient2,123m)
            };

            var sizes = new List<MealSizeCreationInput>
            {
                new ("size",Price.EGP(121),1,ingredientQts,Nutrition.Zero())
            };

            var meal = new MenuMeal(
                categoryId: Guid.NewGuid(),
                restaurantId: Guid.NewGuid(),
                name: "name",
                description: "dfjsa",
                image: "img",
                ingredients: baseIngredients,
                sizes: sizes);

            var command = new AddMealSizeCommand(
                meal.Id,
                "newMealSize",
                2432.2m,
                2,
                new List<IngredientQuantityInput>
                {
                    new IngredientQuantityInput(ingredient1,232),
                    new IngredientQuantityInput(ingredient2,232)
                });

            var ingredientIds = new List<Guid> { ingredient1, ingredient2 };

            _foodServiceMock.Setup(
                f => f.GetFoodNutritionsAsync(
                    It.Is<List<Guid>>(ids => ids.All(id => ingredientIds.Contains(id))),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(ingredientIds.ToDictionary(
                    id => id,
                    id => new FoodNutritionDTO(id, $"ing-{id}", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)));

            _menuMealRepositoryMock.Setup(
                repo => repo.GetByIdAsync(command.MealId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(meal);

            // act
            var act = await _handler.Handle(command,CancellationToken.None);

            //assert
            act.Should().NotBeEmpty();

        }

        private MenuMeal CreateValidMeal()
        {
            var ingredients = new List<MealIngredient>
            {
                new MealIngredient(Guid.NewGuid(), "Meat"),
                new MealIngredient(Guid.NewGuid(), "Cheese"),
            };

            var sizes = new List<MealSizeCreationInput>
            {
                CreateSize("small", 1, ingredients)
            };

            return new MenuMeal(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Meal",
                "Desc",
                "img",
                ingredients,
                sizes
            );
        }
        private MealSizeCreationInput CreateSize(string name, int sortOrder, List<MealIngredient> ingredients)
        {
            var quantities = ingredients
                .Select(i => new IngredientQuantity(i.FoodId, 100))
                .ToList();

            return new MealSizeCreationInput(
                name,
                Price.EGP(100),
                sortOrder,
                quantities,
                Nutrition.Zero()
            );
        }
    }

}
