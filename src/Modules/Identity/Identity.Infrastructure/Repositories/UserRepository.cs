using Identity.Application.Interfaces;
using Identity.Domain.Aggregates;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.ValueObjects;

namespace Identity.Infrastructure.Repositories
{
    internal sealed class UserRepository (IdentityDbContext dbContext) : IUserRepository
    {
        private readonly IdentityDbContext _dbContext = dbContext;

        public void Add(User user)
        {
            _dbContext.Users.Add(user);
        }

        public Task<User?> GetUserByEmailAsync(Email email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.FindAsync(id, cancellationToken);
        }

        public void Remove(User user)
        {
            _dbContext.Users.Remove(user);
        }
    }
}
