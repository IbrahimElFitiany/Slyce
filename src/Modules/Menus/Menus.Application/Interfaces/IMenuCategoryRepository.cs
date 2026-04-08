using Menus.Domain.Entities;

namespace Menus.Application.Interfaces
{
    public interface IMenuCategoryRepository
    {
        Task<MenuCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<MenuCategory>> ListAsync(CancellationToken cancellationToken = default);
        void Add(MenuCategory menuCategory);
        void Delete(Guid id);
    }
}