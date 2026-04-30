using Shared.Domain.Exceptions;

namespace Shared.Domain.ValueObjects
{
    public sealed class Price
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = default!;

        private Price() { }
        private Price(decimal amount, string currency)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Price cannot be negative.");

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required.", nameof(currency));

            Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
            Currency = currency.ToUpperInvariant();
        }

        public static Price EGP(decimal amount) => new Price(amount, "EGP");

        public static Price operator +(Price a, Price b)
        {
            if (a.Currency != b.Currency)
                throw new CurrencyMismatchException();

            return new Price(a.Amount + b.Amount, a.Currency);
        }

        public static Price operator *(Price price, int multiplier)
        {
            return new Price(price.Amount * multiplier, price.Currency);
        }

        public override string ToString() => $"{Amount:0.00} {Currency}";

    }
}