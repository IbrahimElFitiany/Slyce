using Menus.Domain.Exceptions;
using Menus.Domain.ValueObjects;
using Shared.Domain.ValueObjects;

namespace Menus.Domain.Entities
{
    public sealed class MenuMeal
    {
        private const int MealSizesLimit = 5;

        public Guid Id { get; private init; }
        public Guid CategoryId { get; private init; }
        public Guid RestaurantId { get; private init; }
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public string Image { get; private set; } = null!;
        public bool Available { get; private set; } = true;
        public bool Reviewed { get; private set; } = false;

        private readonly List<MealIngredient> _ingredients = new();
        public IReadOnlyCollection<MealIngredient> Ingredients => _ingredients;

        private readonly List<MealSize> _sizes = new();
        public IReadOnlyCollection<MealSize> Sizes => _sizes;

        public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        private MenuMeal() { }
        public MenuMeal(
            Guid categoryId,
            Guid restaurantId,
            string name,
            string description,
            string image,
            IEnumerable<MealIngredient> ingredients,
            IEnumerable<MealSizeCreationInput> sizes,
            bool available = true)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(categoryId, Guid.Empty);
            ArgumentOutOfRangeException.ThrowIfEqual(restaurantId, Guid.Empty);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(description);
            ArgumentException.ThrowIfNullOrWhiteSpace(image);

            if (ingredients == null || !ingredients.Any())
                throw new ArgumentException("Meal must have at least 1 ingredient.");

            if (sizes == null || !sizes.Any())
                throw new ArgumentException("Meal must have at least one size.", nameof(sizes));

            if (sizes.Count() > MealSizesLimit)
                throw new MealSizesLimitExceededException(MealSizesLimit);

            Id = Guid.NewGuid();
            CategoryId = categoryId;
            RestaurantId = restaurantId;
            Name = name;
            Description = description;
            Image = image;
            Available = available;
            _ingredients.AddRange(ingredients);

            var mealIngredientIds = _ingredients.Select(i => i.FoodId).ToHashSet();

            foreach (var size in sizes)
            {
                AddSize(
                    size.Name,
                    size.Price,
                    size.SortOrder,
                    size.Quantities,
                    size.SizeNutrition,
                    mealIngredientIds);
            }
        }

        public void AddSize(
            string name,
            Price price,
            int sortOrder,
            IEnumerable<IngredientQuantity> ingredientQuantities,
            Nutrition sizeNutrition,
            HashSet<Guid>? mealIngredientIds = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfLessThan(sortOrder, 0);

            if (_sizes.Count >= MealSizesLimit)
                throw new MealSizesLimitExceededException(MealSizesLimit);

            if (_sizes.Any(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase) || s.SortOrder == sortOrder))
                throw new DuplicateMealSizeException();

            var ingredientIds = mealIngredientIds ?? _ingredients.Select(i => i.FoodId).ToHashSet();
            var sizeIngredientIds = new HashSet<Guid>(ingredientQuantities.Select(iq => iq.MealIngredientId).ToHashSet());

            if (!sizeIngredientIds.SetEquals(ingredientIds))
                throw new MealSizeIngredientMismatchException(name);

            var mealSize = MealSize.Create(
                name: name,
                price: price,
                sortOrder: sortOrder,
                ingredientQuantities: ingredientQuantities,
                nutrition: sizeNutrition);

            _sizes.Add(mealSize);

            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveSize(Guid sizeId)
        {

            if (_sizes.Count <= 1)
                throw new MinimumMealSizesRequiredException();

            var size = _sizes.FirstOrDefault(s => s.Id == sizeId);

            if (size is null)
                throw new Exception("not Found");

            _sizes.Remove(size);
            UpdatedAt = DateTime.UtcNow;
        }

    }
}