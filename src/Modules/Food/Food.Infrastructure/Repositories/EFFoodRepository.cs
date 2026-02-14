using Food.Application.Interfaces;
using Food.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FoodEntity = Food.Domain.Entities.Food;

namespace Food.Infrastructure.Repositories
{
    internal class EFFoodRepository : IFoodRepository
    {
        private readonly FoodDbContext _db;
        public EFFoodRepository(FoodDbContext dbContext) {
            _db = dbContext;
        }

        public async Task<IEnumerable<FoodEntity>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            var foods = await _db.Foods
                .AsNoTracking()
                .Where(f => ids.Contains(f.Id))
                .ToListAsync();

            return foods;
        }
    }
}
