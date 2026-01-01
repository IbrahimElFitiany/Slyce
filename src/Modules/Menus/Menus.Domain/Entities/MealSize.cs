using Menus.Domain.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Menus.Domain.Entities
{
    public sealed class MealSize
    {
        public Guid Id { get; private init; }
        public Guid MenuItemId { get; private init; }
        public string Name { get; private set; }
        public Price Price { get; private set; }
        public int SortOrder { get; private set; }
        public Nutrition Nutrition { get; private set; }

        public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        private MealSize() { }
        public MealSize(Guid menuItemId, string name, Price price , int sortOrder , Nutrition nutrition)
        {
            if (menuItemId == Guid.Empty)
                throw new ArgumentException("CategoryId is required.", nameof(menuItemId));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Meal name cannot be empty.", nameof(name));

            Id = Guid.NewGuid();
            MenuItemId = menuItemId;
            Name = name;
            Price = price;
            SortOrder = sortOrder;
            Nutrition = nutrition;
        }

    }
}