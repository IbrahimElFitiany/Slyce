using Shared.Domain.ValueObjects;

namespace Food.Domain.Entities
{
    public sealed class Food
    {
        public Guid Id { get; private init; }
        public string Name { get; private set; } = null!;
        public string? Image { get; private set; }
        public Nutrition NutritionPer100g { get; } = null!;
        public string ExternalId { get; init; }
        public string Source { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Food() { }

        public Food(
            string name,
            string imageUrl,
            Nutrition nutrition,
            string externalId,
            string source)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            ArgumentNullException.ThrowIfNull(nutrition, nameof(nutrition));
            ArgumentException.ThrowIfNullOrWhiteSpace(externalId, nameof(externalId));
            ArgumentException.ThrowIfNullOrWhiteSpace(source, nameof(source));

            Id = Guid.NewGuid();
            Name = name;
            Image = imageUrl;
            NutritionPer100g = nutrition;
            ExternalId = externalId;
            Source = source;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }
    }
}