using Food.Contracts.DTOs;

namespace Food.Contracts
{
    public interface IFoodServices
    {
        /// <summary>
        /// Retrieves the nutrition information for the specified food items.
        /// </summary>
        /// <param name="foodIds">The IDs of the food items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A dictionary where each key is a food ID and the value is a <see cref="FoodNutritionDTO"/> containing
        /// the nutrition details per 100g of that food.
        /// </returns>
        Task<Dictionary<Guid, FoodNutritionDTO>> GetFoodNutritionsAsync(IEnumerable<Guid> foodIds, CancellationToken cancellationToken);
    }
}
