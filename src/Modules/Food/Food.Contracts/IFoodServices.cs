using Food.Contracts.DTOs;

namespace Food.Contracts
{
    public interface IFoodServices
    {
        Task<Dictionary<Guid, FoodNutritionDTO>> GetFoodNutritionsAsync(IEnumerable<Guid> foodIds, CancellationToken cancellationToken);

    }
}
