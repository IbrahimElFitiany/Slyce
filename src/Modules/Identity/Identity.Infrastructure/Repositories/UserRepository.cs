using Identity.Domain.Aggregates;
using Identity.Domain.Repositories;
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

        public async Task<User?> GetUserByEmailAsync(Email email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
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