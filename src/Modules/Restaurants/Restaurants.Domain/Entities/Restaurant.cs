using Restaurants.Domain.Enums;

namespace Restaurants.Domain.Entities
{
    public sealed class Restaurant
    {
        public Guid Id { get; private init; }
        public string? Image { get; private set; }
        public string BrandName { get; private set; } = null!;
        public string? Description { get; private set; }
        public RestaurantType Type { get; private set; }
        public RestaurantStatus Status { get; private set; } = RestaurantStatus.Draft;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Restaurant() { }
        public Restaurant(
            string brandName,
            string? image,
            string? description,
            RestaurantType restaurantType)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(brandName);

            if (description is not null)
                ArgumentException.ThrowIfNullOrWhiteSpace(description);

            if (image is not null)
                ArgumentException.ThrowIfNullOrWhiteSpace(image);

            Id = Guid.NewGuid();
            BrandName = brandName;
            Image = image;
            Description = description;
            Type = restaurantType;

            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus(RestaurantStatus status)
        {
            Status = status;
            UpdatedAt = DateTime.UtcNow;
        } 

    }
}