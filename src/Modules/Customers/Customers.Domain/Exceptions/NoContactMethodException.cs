using Shared.Domain.Exceptions;

namespace Customers.Domain.Exceptions
{
    public sealed class NoContactMethodException : DomainException
    {
        public NoContactMethodException() : base("Customer must have at least one contact method.") { }
    }
}
