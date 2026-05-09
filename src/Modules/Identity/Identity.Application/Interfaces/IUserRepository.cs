using Identity.Domain.Aggregates;
using Shared.Domain.ValueObjects;

namespace Identity.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<User?> GetUserByEmailAsync(Email email, CancellationToken cancellationToken);

        void Add(User user);

        void Remove(User user);

    }
}