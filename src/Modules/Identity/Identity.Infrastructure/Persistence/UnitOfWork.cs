using Identity.Application.Interfaces;

namespace Identity.Infrastructure.Persistence
{
    internal sealed class UnitOfWork (IdentityDbContext dbContext) : IUnitOfWork
    {
        private readonly IdentityDbContext _dbContext = dbContext;

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
