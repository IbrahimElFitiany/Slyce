namespace Restaurants.Domain.Entities
{
    public enum RestaurantStatus
    {
        Pending,
        Approved,
        Suspended
    }
    public class Restaurant
    {
        public Guid Id { get; private set; }
        public string Image { get; private set; } = string.Empty;
        public string Name { get; private set; }
        public string Description { get; private set; }
        public RestaurantStatus Status { get; private set; } = RestaurantStatus.Pending;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        public Restaurant(string name, string description, string image)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Image = image;
        }

    }
}
