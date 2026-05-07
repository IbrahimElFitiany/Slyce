using Identity.Domain.Aggregates;

namespace Identity.Application.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}