using Restaurants.Domain.Enums;

namespace Restaurants.Domain.Entities
{
    public sealed class Restaurant
    {
        public Guid Id { get; private init; }
        public string? Image { get; private set; }
        public string BrandName { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public RestaurantType Type { get; private set; }
        public RestaurantStatus Status { get; private set; } = RestaurantStatus.Pending;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        private Restaurant() { }
        public Restaurant(string name, string description, RestaurantType restaurantType)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(description);

            Id = Guid.NewGuid();
            BrandName = name;
            Description = description;
            Type = restaurantType;
        }

        public void UpdateStatus(RestaurantStatus status)
        {
            Status = status;
            UpdatedAt = DateTime.UtcNow;
        } 
    }
}