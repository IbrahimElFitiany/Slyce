using Menus.Application.Interfaces;
using Menus.Domain.Entities;
using Menus.Infrastructure.Persistence;

namespace Menus.Infrastructure.Repositories
{
    public class EFMenuCategoryRepository : IMenuCategoryRepository
    {
        private readonly MenusDbContext _db;
        public EFMenuCategoryRepository (MenusDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(MenuCategory menuCategory, CancellationToken ct)
        {
            await _db.MenuCategories.AddAsync(menuCategory, ct);
            await _db.SaveChangesAsync();
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<MenuCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MenuCategory>> ListAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}