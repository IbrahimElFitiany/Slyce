using Menus.Contracts.DTOs;

namespace Menus.Contracts.Interfaces
{
    public interface IMenuQueryServices
    {
        /// <summary>
        /// Validates that the provided meal size IDs exist and belong to the specified restaurant,
        /// then resolves and returns their prices.
        /// </summary>
        /// <returns>
        /// A read-only collection containing the resolved meal size data and prices.
        /// </returns>
        Task<IReadOnlyCollection<MealSizeDTO>> ValidateMealSizesAndGetPricesAsync(IEnumerable<Guid> sizeIds, Guid restaurantId, CancellationToken cancellationToken);
    }
}
