namespace Shared.Kernal.Exceptions
{
    public class DomainException : Exception
    {
        protected DomainException(string message) : base(message) { }
    }
}
