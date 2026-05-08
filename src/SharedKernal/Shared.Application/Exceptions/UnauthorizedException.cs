namespace Shared.Application.Exceptions
{

    public sealed class UnauthorizedException(string message) : ApplicationException(message);
}