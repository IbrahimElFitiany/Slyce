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
                .Where(f => ids.Contains(f.Id))
                .ToListAsync();

            return foods;
        }

        public async Task InsertIfNotExistsAsync(IEnumerable<FoodEntity> foods, CancellationToken ct)
        {
            var externalIds = foods.Select(f => f.ExternalId).ToList();

            var existingIds = await _db.Foods
                .Where(f => externalIds.Contains(f.ExternalId))
                .Select(f => f.ExternalId)
                .ToHashSetAsync(ct);

            var newFoods = foods.Where(f => !existingIds.Contains(f.ExternalId));

            _db.Foods.AddRange(newFoods);
        }
    }
}
