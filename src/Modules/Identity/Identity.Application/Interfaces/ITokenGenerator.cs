using Identity.Domain.Aggregates;

namespace Identity.Application.Interfaces
{
    public interface ITokenGenerator
    {
        (string, DateTime) GenerateToken(User user);
    }
}