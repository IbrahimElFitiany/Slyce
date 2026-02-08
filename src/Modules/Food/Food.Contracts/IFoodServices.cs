using Food.Contracts.DTOs;

namespace Food.Contracts
{
    public interface IFoodServices
    {
        Task<IEnumerable<FoodNutritionDTO>> GetFoodNutritionsAsync(IEnumerable<Guid> foodIds, CancellationToken cancellationToken);

    }
}
