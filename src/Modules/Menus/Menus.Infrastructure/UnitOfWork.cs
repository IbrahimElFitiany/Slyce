using Menus.Application.Interfaces;
using Menus.Infrastructure.Persistence;

namespace Menus.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly MenusDbContext _menusDbContext;

        public UnitOfWork(MenusDbContext menusDbContext)
        {
            _menusDbContext = menusDbContext;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           await _menusDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}