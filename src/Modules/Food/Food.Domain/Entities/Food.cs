using Shared.Domain.ValueObjects;

namespace Food.Domain.Entities
{
    public sealed class Food
    {
        public Guid Id { get; private init; }
        public string Name { get; private set; } = null!;
        public string Image { get; private set; } = null!;
        public Nutrition NutritionPer100g { get; } = null!;
        public string Source { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Food() { }

        public Food(string name, string imageUrl, Nutrition nutrition, string source)
        {
            Id = Guid.NewGuid();

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Image = imageUrl;
            NutritionPer100g = nutrition;
            Source = source;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public Nutrition NutritionForGrams(decimal grams)
        {
            decimal factor = grams / 100m;
            return NutritionPer100g.Multiply(factor);
        }
    }
}