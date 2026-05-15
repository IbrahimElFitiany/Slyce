using FoodEntity = Food.Domain.Entities.Food;

namespace Food.Application.Interfaces
{
    public interface IFoodRepository
    {
        Task<IReadOnlyList<FoodEntity>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<FoodEntity>> GetByExternalId(IEnumerable<string> externalIds, CancellationToken cancellationToken);
        Task UpsertRange(IEnumerable<FoodEntity> foods, CancellationToken cancellationToken);
    }
}
