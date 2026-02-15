namespace Shared.Domain.ValueObjects
{
    public sealed record Email
    {
        public string Value { get; private init; }
        private Email(string value) {
            Value = value;
        }

        public static Email Create(string value) {

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be empty.");

            if (!value.Contains('@') || !value.Contains('.'))
                throw new ArgumentException("Invalid email format.");

            return new Email(value);
        }
        public override string ToString() => Value;

    }
}