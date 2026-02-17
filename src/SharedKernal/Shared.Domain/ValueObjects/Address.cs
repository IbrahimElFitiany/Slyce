namespace Shared.Domain.ValueObjects
{
    public record Address
    {
        public string City { get; } = null!;
        public string Area { get; } = null!;
        public string? StreetName { get; }
        public int? StreetNumber { get; }
        public Coordinates Coordinates { get; } = null!;

        private Address(){ }
        public Address (
            string city,
            string area,
            string? street,
            int? streetNumber,
            Coordinates coordinates)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(city);
            ArgumentException.ThrowIfNullOrWhiteSpace(area);
            ArgumentNullException.ThrowIfNull(coordinates);

            City = city.Trim().ToLower();
            Area = area.Trim().ToLower();
            StreetName = street;
            StreetNumber = streetNumber;
            Coordinates = coordinates;
        }
    }
}
