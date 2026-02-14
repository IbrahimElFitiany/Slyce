using Menus.Domain.Entities;

namespace Menus.Application.Interfaces
{
    public interface IMenuMealRepository
    {
        Task<MenuMeal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameInRestaurantAsync(string name, Guid restaurantId,  CancellationToken cancellationToken = default);
        Task AddAsync(MenuMeal menuMeal, CancellationToken cancellationToken = default);
        Task<IEnumerable<MenuMeal>> ListAsync(CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}