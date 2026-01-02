using System.ComponentModel;

namespace Customers.Domain.ValueObjects
{
    public sealed class Height
    {
        public int Centimeters { get; private set; }

        private Height(int centimeters) {

            if (centimeters <= 0) throw new InvalidEnumArgumentException();

            Centimeters = centimeters; 
        }
        public static Height FromCm(int centimeters)
        {
            return new Height(centimeters);
        }
    }
}
