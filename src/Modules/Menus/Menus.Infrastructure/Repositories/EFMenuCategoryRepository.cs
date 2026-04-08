using Menus.Application.Interfaces;
using Menus.Domain.Entities;
using Menus.Infrastructure.Persistence;

namespace Menus.Infrastructure.Repositories
{
    public class EFMenuCategoryRepository : IMenuCategoryRepository
    {
        private readonly MenusDbContext _dbContext;
        public EFMenuCategoryRepository (MenusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(MenuCategory menuCategory)
        {
            _dbContext.MenuCategories.AddAsync(menuCategory);
        }

        public void Delete(Guid id)
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
    }
}