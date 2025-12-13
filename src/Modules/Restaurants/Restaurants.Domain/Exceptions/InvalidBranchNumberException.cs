namespace Restaurants.Domain.Exceptions
{

    public sealed class InvalidBranchNumberException : DomainException
    {
        public InvalidBranchNumberException(int branches)
            : base($"Branch number must be at least 1, got {branches}.") { }
    }

}
