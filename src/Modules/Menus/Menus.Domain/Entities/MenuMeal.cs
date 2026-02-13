using Menus.Domain.Exceptions;
using Menus.Domain.ValueObjects;

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
            List<MealIngredient> ingredients,
            List<MealSize> sizes,
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

            if (sizes.GroupBy(s => s.Name).Any(g => g.Count() > 1))
                throw new DuplicateMealSizeException();

            if (sizes.Count > MealSizesLimit)
                throw new MealSizesLimitExceededException(MealSizesLimit);

            Id = Guid.NewGuid();
            CategoryId = categoryId;
            RestaurantId = restaurantId;
            Name = name;
            Description = description;
            Image = image;
            Available = available;
            _ingredients.AddRange(ingredients);

            var validIngredientIds = _ingredients.Select(i => i.FoodId).ToHashSet();

            foreach (var size in sizes)
            {
                ValidateSizeIngredients(size,validIngredientIds);
            }

            _sizes.AddRange(sizes);
        }

        public void AddSize(MealSize size)
        {
            ArgumentNullException.ThrowIfNull(size);

            if (_sizes.Count >= MealSizesLimit)
                throw new MealSizesLimitExceededException(MealSizesLimit);

            if (_sizes.Any(s => s.Name.Equals(size.Name, StringComparison.OrdinalIgnoreCase)))
                throw new DuplicateMealSizeException();

            var validIngredientIds = _ingredients.Select(i => i.FoodId).ToHashSet();

            ValidateSizeIngredients(size,validIngredientIds);

            _sizes.Add(size);

            UpdatedAt = DateTime.UtcNow;
        }
        public void RemoveSize(MealSize size)
        {
            ArgumentNullException.ThrowIfNull(size);

            if (_sizes.Count <= 1)
                throw new MinimumMealSizesRequiredException();

            _sizes.Remove(size);
            UpdatedAt = DateTime.UtcNow;
        }


        private void ValidateSizeIngredients(MealSize mealSize, HashSet<Guid> validIngredientIds)
        {
            var sizeIngredientIds = new HashSet<Guid>(mealSize.IngredientQuantities.Select(iq => iq.MealIngredientId));

            if (!sizeIngredientIds.SetEquals(validIngredientIds))
                throw new MealSizeIngredientMismatchException(mealSize.Name);
        }
    }
}