using Shared.Domain.Exceptions;

namespace Orders.Domain.Exceptions
{
    public sealed class CartBranchMismatchException : DomainException
    {
        public CartBranchMismatchException()
            : base("Cannot mix items from different branches in the same cart.") { }

        public CartBranchMismatchException(Guid cartBranchId, Guid itemBranchId)
            : base($"Cart branch '{cartBranchId}' does not match item branch '{itemBranchId}'.") { }
    }
}