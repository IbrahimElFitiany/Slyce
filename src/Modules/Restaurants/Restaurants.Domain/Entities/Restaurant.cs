using Restaurants.Domain.Enums;
using Shared.Domain.Common;

namespace Restaurants.Domain.Entities
{
    public sealed class Restaurant : AggregateRoot
    {
        public string? Logo { get; private set; } = null;
        public string BrandName { get; private set; } = null!;
        public string? Banner { get; private set; } = null;
        public string? Description { get; private set; }
        public RestaurantType Type { get; private set; }
        public RestaurantStatus Status { get; private set; } = RestaurantStatus.UnderReview;

        private Restaurant() { }
        public Restaurant(
            string brandName,
            string? description,
            RestaurantType restaurantType)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(brandName);

            if (description is not null)
                ArgumentException.ThrowIfNullOrWhiteSpace(description);

            Id = Guid.NewGuid();
            BrandName = brandName;
            Description = description;
            Type = restaurantType;

            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus(RestaurantStatus status)
        {
            Status = status;
            UpdatedAt = DateTime.UtcNow;
        }
        public void UpdateLogo(string logo)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(logo);

            Logo = logo;
            UpdatedAt = DateTime.UtcNow;
        }
        public void UpdateBanner(string banner)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(banner);

            Banner = banner;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}