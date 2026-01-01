namespace Menus.Domain.Entities
{
    public sealed class MenuMeal
    {
        public Guid Id { get; private init; }
        public Guid CategoryId { get; private init; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public bool Available { get; private set; } = true;
        public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        public MenuMeal(Guid categoryId, string name, string description, string image, bool available = true)
        {
            if (categoryId == Guid.Empty)
                throw new ArgumentException("CategoryId is required.", nameof(categoryId));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Meal name cannot be empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Meal description cannot be empty.", nameof(description));

            if (string.IsNullOrWhiteSpace(image))
                throw new ArgumentException("Meal image cannot be empty.", nameof(image));


            Id = Guid.NewGuid();
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Image = image;
            Available = available;
        }

    }
}