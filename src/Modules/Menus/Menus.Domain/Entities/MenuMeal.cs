using Menus.Domain.Exceptions;

namespace Menus.Domain.Entities
{
    public sealed class MenuMeal
    {
        private const int MealSizesLimit = 5;

        public Guid Id { get; private init; }
        public Guid CategoryId { get; private init; }
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public string Image { get; private set; } = null!;
        public bool Available { get; private set; } = true;

        private readonly List<MealSize> _sizes = new();
        public IReadOnlyCollection<MealSize> Sizes => _sizes;
        public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        private MenuMeal() { }
        public MenuMeal(
            Guid categoryId,
            string name,
            string description,
            string image,
            List<MealSize> sizes,
            bool available = true)
        {
            if (categoryId == Guid.Empty)
                throw new ArgumentException("CategoryId is required.", nameof(categoryId));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Meal name cannot be empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Meal description cannot be empty.", nameof(description));

            if (string.IsNullOrWhiteSpace(image))
                throw new ArgumentException("Meal image cannot be empty.", nameof(image));

            if (sizes == null || !sizes.Any())
                throw new ArgumentException("Meal must have at least one size.", nameof(sizes));

            if (sizes.GroupBy(s => s.Name).Any(g => g.Count() > 1))
                throw new DuplicateMealSizeException();

            Id = Guid.NewGuid();
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Image = image;
            Available = available;

            if (sizes.Count > MealSizesLimit)
                throw new MealSizesLimitExceededException(MealSizesLimit);

            _sizes.AddRange(sizes);
        }

        public void AddSize(MealSize size)
        {
            if (size is null)
                throw new ArgumentNullException(nameof(size));

            if (_sizes.Any(s => s.Name.Equals(size.Name, StringComparison.OrdinalIgnoreCase)))
                throw new DuplicateMealSizeException();

            if (_sizes.Count >= MealSizesLimit)
                throw new MealSizesLimitExceededException(MealSizesLimit);

            _sizes.Add(size);

            UpdatedAt = DateTime.UtcNow;
        }
        public void RemoveSize(MealSize size)
        {
            if (size is null)
                throw new ArgumentNullException(nameof(size));

            if (_sizes.Count <= 1)
                throw new MinimumMealSizesRequiredException();

            _sizes.Remove(size);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}