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

        public async Task AddAsync(MenuMeal menuMeal, CancellationToken ct)
        {
            await _dbContext.MenuMeals.AddAsync(menuMeal, ct);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsByNameInRestaurantAsync(string name, Guid restaurantId, CancellationToken ct = default)
        {
            return await _dbContext.MenuMeals
                .AnyAsync(m => m.Name == name && m.RestaurantId == restaurantId, ct);
        }

        public Task<MenuMeal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MenuMeal>> ListAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}