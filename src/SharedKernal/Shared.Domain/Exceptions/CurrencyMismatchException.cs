namespace Shared.Domain.Exceptions
{
    internal class CurrencyMismatchException : DomainException
    {
        public CurrencyMismatchException() : base("Cannot perform operations on money with different currencies.") { }
    }
}
