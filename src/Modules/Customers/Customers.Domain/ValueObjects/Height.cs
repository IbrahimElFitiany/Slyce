namespace Customers.Domain.ValueObjects
{
    public sealed record Height
    {
        public int Centimeters { get; private set; }

        private Height(int centimeters) {

            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(centimeters, 0);

            Centimeters = centimeters; 
        }

        public static Height FromCm(int centimeters)
        {
            return new Height(centimeters);
        }
    }
}
