namespace Menus.Domain.Entities
{
    public sealed class MenuCategory
    {
        public Guid Id { get; private init; }
        public Guid RestaurantId { get; private init; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        public MenuCategory(Guid restaurantId, string name) {

            if (restaurantId == Guid.Empty)
                throw new ArgumentException("RestaurantId is required.", nameof(restaurantId));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty.", nameof(name));

            Id = Guid.NewGuid();
            RestaurantId = restaurantId;
            Name = name;
        }
    }
}
