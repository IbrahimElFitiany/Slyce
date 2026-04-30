using Menus.Application.Interfaces;
using Menus.Domain.Entities;
using Menus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Menus.Infrastructure.Repositories
{
    public class EFMenuMealRepository : IMenuMealRepository
    {
        private readonly MenusDbContext _dbContext;
        public EFMenuMealRepository(MenusDbContext db)
        {
            _dbContext = db;
        }

        public void Add(MenuMeal menuMeal)
        {
            _dbContext.MenuMeals.Add(menuMeal);
        }

        public void Delete(Guid id)
        {
            _dbContext.Remove(id);
        }

        public async Task<bool> ExistsByNameInRestaurantAsync(string name, Guid restaurantId, CancellationToken ct = default)
        {
            return await _dbContext.MenuMeals
                .AnyAsync(m => m.Name == name && m.RestaurantId == restaurantId, ct);
        }

        public async Task<MenuMeal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.MenuMeals.FindAsync(id, cancellationToken);
        }

        public Task<IEnumerable<MenuMeal>> ListAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}