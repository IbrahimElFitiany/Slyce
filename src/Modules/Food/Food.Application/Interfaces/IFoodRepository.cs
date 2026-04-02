using FoodEntity = Food.Domain.Entities.Food;


namespace Food.Application.Interfaces
{
    public interface IFoodRepository
    {
        Task<IEnumerable<FoodEntity>> GetByIdsAsync(IEnumerable<Guid> ids,CancellationToken cancellationToken = default);
        Task InsertIfNotExistsAsync(IEnumerable<FoodEntity> foods, CancellationToken ct);
    }
}
