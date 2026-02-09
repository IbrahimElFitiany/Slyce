using Shared.Kernal.ValueObjects;

namespace Menus.Domain.Entities
{
    public sealed class MealSize
    {
        public Guid Id { get; private init; }
        public string Name { get; private set; } = null!;
        public Price Price { get; private set; } = null!;
        public int SortOrder { get; private set; }
        public Nutrition Nutrition { get; private set; } = null!;
        public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        private MealSize() { }
        public MealSize(string name, Price price , int sortOrder , Nutrition nutrition)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Meal name cannot be empty.", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            SortOrder = sortOrder;
            Nutrition = nutrition;
        }

    }
}