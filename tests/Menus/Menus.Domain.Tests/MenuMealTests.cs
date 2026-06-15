using FluentAssertions;
using Menus.Domain.Entities;
using Menus.Domain.Exceptions;
using Menus.Domain.ValueObjects;
using Shared.Domain.ValueObjects;


namespace Menus.Domain.Tests
{
    public class MenuMealTests
    {
        [Fact]
        public void AddSize_WhenSizeIsValid_ShouldAddSizeToMealSizes()
        {
            //arrange
            var meal = CreateValidMeal();
            var quantities = meal.Ingredients
            .Select(i => new IngredientQuantity(i.FoodId, 100))
            .ToList();

            //act 
            meal.AddSize("extra", Price.EGP(200), 2, quantities, Nutrition.Zero());
            meal.Sizes.Should().HaveCount(2);
            meal.Sizes.Any(s => s.Name == "extra").Should().BeTrue();
        }

        [Fact]
        public void AddSize_WithDuplicateName_Throws()
        {
            var meal = CreateValidMeal();
            var existing = meal.Sizes.First();

            var quantities = meal.Ingredients
                .Select(i => new IngredientQuantity(i.FoodId, 100));

            Action act = () => meal.AddSize(
                existing.Name,
                Price.EGP(100),
                99,
                quantities,
                Nutrition.Zero()
            );

            act.Should().Throw<DuplicateMealSizeException>();
        }

        [Fact]
        public void AddSize_WithDuplicateSortOrder_Throws()
        {
            var meal = CreateValidMeal();
            var existing = meal.Sizes.First();

            var quantities = meal.Ingredients
                .Select(i => new IngredientQuantity(i.FoodId, 100));

            Action act = () => meal.AddSize(
                "new",
                Price.EGP(100),
                existing.SortOrder,
                quantities,
                Nutrition.Zero()
            );

            act.Should().Throw<DuplicateMealSizeException>();
        }

        [Fact]
        public void AddSize_WhenExceedLimit_Throws()
        {
            var meal = CreateMealWithMaxSizes();

            var quantities = meal.Ingredients
                .Select(i => new IngredientQuantity(i.FoodId, 100));

            Action act = () => meal.AddSize(
                "overflow",
                Price.EGP(100),
                999,
                quantities,
                Nutrition.Zero()
            );

            act.Should().Throw<MealSizesLimitExceededException>();
        }

        [Fact]
        public void AddSize_WithMismatchedIngredients_Throws()
        {
            var meal = CreateValidMeal();

            var wrong = new List<IngredientQuantity>
            {
                new IngredientQuantity(Guid.NewGuid(), 100)
            };

            Action act = () => meal.AddSize(
                "bad",
                Price.EGP(100),
                99,
                wrong,
                Nutrition.Zero()
            );

            act.Should().Throw<MealSizeIngredientMismatchException>();
        }

        [Fact]
        public void AddSize_ShouldUpdateUpdatedAt()
        {
            var meal = CreateValidMeal();
            var before = meal.UpdatedAt;

            var quantities = meal.Ingredients
                .Select(i => new IngredientQuantity(i.FoodId, 100));

            meal.AddSize("new", Price.EGP(100), 99, quantities, Nutrition.Zero());

            meal.UpdatedAt.Should().BeAfter(before);
        }


        [Fact]
        public void Ctor_WhenValidInputs_CreateMealWithCorrectState()
        {
            var categoryId = Guid.NewGuid();
            var restaurantId = Guid.NewGuid();
            var baseIngredients = new List<MealIngredient>{
                new MealIngredient(Guid.Parse("1afec456-af18-4e07-87bc-eb6b7cf0e7e4"), "Meat"),
                new MealIngredient(Guid.Parse("1309ba6c-6084-4f3d-9463-c0682d96fd27"), "Cheese"),
                new MealIngredient(Guid.Parse("bbd40d5a-efb3-4564-9197-5530402b8d81"), "Buns"),
            };
            var mealSizes = new List<MealSizeCreationInput>
            {
                new MealSizeCreationInput(
                    Name: "small",
                    Price: Price.EGP(200),
                    SortOrder: 1,
                    Quantities: new List<IngredientQuantity>
                    {
                        new IngredientQuantity(Guid.Parse("1afec456-af18-4e07-87bc-eb6b7cf0e7e4"),200),
                        new IngredientQuantity(Guid.Parse("1309ba6c-6084-4f3d-9463-c0682d96fd27"),50),
                        new IngredientQuantity(Guid.Parse("bbd40d5a-efb3-4564-9197-5530402b8d81"),150),
                    },
                    SizeNutrition: Nutrition.Zero()),
                new MealSizeCreationInput(
                    Name: "medium",
                    Price: Price.EGP(300),
                    SortOrder: 2,
                    Quantities: new List<IngredientQuantity>
                    {
                        new IngredientQuantity(Guid.Parse("1afec456-af18-4e07-87bc-eb6b7cf0e7e4"),300),
                        new IngredientQuantity(Guid.Parse("1309ba6c-6084-4f3d-9463-c0682d96fd27"),50),
                        new IngredientQuantity(Guid.Parse("bbd40d5a-efb3-4564-9197-5530402b8d81"),150),
                    },
                    SizeNutrition: Nutrition.Zero()),
                new MealSizeCreationInput(
                    Name: "large",
                    Price: Price.EGP(400),
                    SortOrder: 3,
                    Quantities: new List<IngredientQuantity>
                    {
                        new IngredientQuantity(Guid.Parse("1afec456-af18-4e07-87bc-eb6b7cf0e7e4"),350),
                        new IngredientQuantity(Guid.Parse("1309ba6c-6084-4f3d-9463-c0682d96fd27"),50),
                        new IngredientQuantity(Guid.Parse("bbd40d5a-efb3-4564-9197-5530402b8d81"),150),
                    },
                    SizeNutrition: Nutrition.Zero())
                };

            var meal = new MenuMeal(
                categoryId: categoryId,
                restaurantId: restaurantId,
                name: "Burger",
                description: "Classic cheeseburger",
                image: "url_here",
                ingredients: baseIngredients,
                sizes: mealSizes,
                available: true
            );

            meal.Sizes.Count.Should().Be(3);
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
        private MenuMeal CreateMealWithMaxSizes()
        {
            var ingredients = new List<MealIngredient>
            {
                new MealIngredient(Guid.NewGuid(), "Meat"),
                new MealIngredient(Guid.NewGuid(), "Cheese"),
            };
            var sizes = new List<MealSizeCreationInput>
            {
                CreateSize("small", 1, ingredients),
                CreateSize("medium", 2, ingredients),
                CreateSize("large", 3, ingredients),
                CreateSize("xlarge", 4, ingredients),
                CreateSize("xxlarge", 5, ingredients),
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

    }
}