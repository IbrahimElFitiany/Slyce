using Menus.Domain.Entities;

namespace Menus.Application.Interfaces
{
    public interface IMenuCategoryRepository
    {
        Task<MenuCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(MenuCategory menuCategory, CancellationToken cancellationToken = default);
        Task<IEnumerable<MenuCategory>> ListAsync(CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}