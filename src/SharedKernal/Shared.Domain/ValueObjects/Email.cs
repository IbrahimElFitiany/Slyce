using System.Net.Mail;

namespace Shared.Domain.ValueObjects
{
    public sealed record Email
    {
        public string Value { get; private init; }
        private Email(string value) {

            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            value = value.Trim().ToLowerInvariant();

            try
            {
                _ = new MailAddress(value);
            }
            catch
            {
                throw new ArgumentException("Invalid email format.");
            }

            Value = value;
        }

        public static Email Create(string value) {

            return new Email(value);
        }

        public override string ToString() => Value;

    }
}