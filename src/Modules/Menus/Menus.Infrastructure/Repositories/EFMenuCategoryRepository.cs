using Menus.Domain.Entities;
using Menus.Domain.Repositories;
using Menus.Infrastructure.Persistence;

namespace Menus.Infrastructure.Repositories
{
    public sealed class EFMenuCategoryRepository(MenusDbContext dbContext) : IMenuCategoryRepository
    {

        public void Add(MenuCategory menuCategory)
        {
            dbContext.MenuCategories.Add(menuCategory);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<MenuCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.MenuCategories.FindAsync(id, cancellationToken);
        }

        public Task<IEnumerable<MenuCategory>> ListAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}