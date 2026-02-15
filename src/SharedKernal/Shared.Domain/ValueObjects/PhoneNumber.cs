namespace Shared.Domain.ValueObjects
{
    public record PhoneNumber
    {
        public string Value { get; init; }

        private PhoneNumber(string value) {
            Value = value;
        }

        public static PhoneNumber Create(string value) {

            value = value.Trim();

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Phone number cannot be empty.");

            if (value.Length < 8 || value.Length > 15)
                throw new ArgumentException("Phone number length is invalid.");

            string digitsPart = value.StartsWith("+") ? value[1..] : value;

            if (!digitsPart.All(char.IsDigit))
                throw new ArgumentException("Phone number can only contain numbers");

            return new PhoneNumber(value);
        }

        public override string ToString() => Value;
    }
}
