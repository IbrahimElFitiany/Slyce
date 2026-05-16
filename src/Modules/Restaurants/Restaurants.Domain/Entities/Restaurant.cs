using Restaurants.Domain.Enums;
using Shared.Domain.Common;

namespace Restaurants.Domain.Entities
{
    public sealed class Restaurant : AggregateRoot
    {
        public string? Image { get; private set; }
        public string BrandName { get; private set; } = null!;
        public string? Description { get; private set; }
        public RestaurantType Type { get; private set; }
        public RestaurantStatus Status { get; private set; } = RestaurantStatus.Draft;

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
        public void UpdateLogo(string image)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(image);

            Image = image;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}