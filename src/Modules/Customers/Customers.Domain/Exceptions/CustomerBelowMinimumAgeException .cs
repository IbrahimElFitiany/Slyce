using Shared.Domain.Exceptions;

namespace Customers.Domain.Exceptions
{
    public sealed class CustomerBelowMinimumAgeException : DomainException
    {
        public CustomerBelowMinimumAgeException() : base($"Customer age is below the minimum required age.") { }

        public CustomerBelowMinimumAgeException(int customerAge , int minAge)
            : base($"Customer age {customerAge} is below the minimum required age of {minAge}.") { }
    }
}
