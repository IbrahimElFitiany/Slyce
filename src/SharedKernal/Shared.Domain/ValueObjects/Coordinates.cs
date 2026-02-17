namespace Shared.Domain.ValueObjects
{
    public record Coordinates {

        public double Latitude { get; }
        public double Longitude { get; }

        private Coordinates() { }
        public Coordinates(double latitude, double longitude)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(latitude, 90);
            ArgumentOutOfRangeException.ThrowIfLessThan(latitude, -90);

            ArgumentOutOfRangeException.ThrowIfGreaterThan(longitude, 180);
            ArgumentOutOfRangeException.ThrowIfLessThan(longitude, -180);

            Latitude = latitude;
            Longitude = longitude;
        }

    }
}
