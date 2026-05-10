namespace Customers.Domain.ValueObjects
{
    public sealed class Weight
    {
        public decimal Kilograms { get; }

        private Weight(decimal kilograms)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(kilograms, 0m);
            Kilograms = kilograms;
        }
        public static Weight FromKilograms(decimal kilograms)
        {
            return new Weight(kilograms);
        }


        public override bool Equals(object? obj) => obj is Weight other && Kilograms == other.Kilograms;

        public override int GetHashCode() => Kilograms.GetHashCode();

    }
}