using Menus.Domain.Exceptions;
using Menus.Domain.ValueObjects;
using Shared.Domain.ValueObjects;

namespace Menus.Domain.Entities
{
    public sealed class MealSize
    {
        public Guid Id { get; private init; }
        public string Name { get; private set; } = null!;
        public Price Price { get; private set; } = null!;
        public int SortOrder { get; private set; }

        private readonly List<IngredientQuantity> _ingredientQuantities = new();
        public IReadOnlyCollection<IngredientQuantity> IngredientQuantities => _ingredientQuantities;

        public Nutrition Nutrition { get; private set; } = null!;
        public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        private MealSize() { }
        static internal MealSize Create(
            string name,
            Price price,
            int sortOrder,
            IEnumerable<IngredientQuantity> ingredientQuantities,
            Nutrition nutrition)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfLessThan(sortOrder, 0);

            if (ingredientQuantities == null || !ingredientQuantities.Any())
                throw new MealSizeMustHaveIngredientsException();

            var size = new MealSize
            {
                Id = Guid.NewGuid(),
                Name = name,
                Price = price,
                SortOrder = sortOrder,
                Nutrition = nutrition,
            };
            size._ingredientQuantities.AddRange(ingredientQuantities);

            return size;
        }
    }
}