using Restaurants.Domain.Enums;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Domain.Entities
{
    public sealed class Restaurant
    {
        public Guid Id { get; private init; }
        public string? Image { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public RestaurantType Type { get; private set; }
        public RestaurantStatus Status { get; private set; } = RestaurantStatus.Pending;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
        
        private Restaurant() { }
        public Restaurant(string name, string description, RestaurantType restaurantType)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidRestaurantNameException(name);

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Restaurant description is required.", nameof(description));

            Id = Guid.NewGuid();
            Name = name;
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