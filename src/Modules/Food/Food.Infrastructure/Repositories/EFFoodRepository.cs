using Food.Application.Interfaces;
using Food.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FoodEntity = Food.Domain.Entities.Food;

namespace Food.Infrastructure.Repositories
{
    internal sealed class EFFoodRepository(FoodDbContext dbContext) : IFoodRepository
    {
        public async Task<IReadOnlyList<FoodEntity>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            var foods = await dbContext.Foods
                .Where(f => ids.Contains(f.Id))
                .ToListAsync(cancellationToken);

            return foods;
        }
        public async Task<IReadOnlyList<FoodEntity>> GetByExternalId(IEnumerable<string> externalIds, CancellationToken ct)
        {
            return await dbContext.Foods
                .Where(f => externalIds.Contains(f.ExternalId))
                .ToListAsync(ct);
        }
        public async Task UpsertRange(IEnumerable<FoodEntity> foods, CancellationToken ct)
        {
            await dbContext.Foods
                .UpsertRange(foods)
                .On(f => new { f.Source, f.ExternalId })
                .NoUpdate()
                .RunAsync(ct);
        }

    }
}
