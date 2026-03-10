using Menus.Contracts.DTOs;

namespace Menus.Contracts.Interfaces
{
    public interface IMenuQueryServices
    {
        /// <summary>
        /// Validates that the provided meal size IDs exist and belong to the specified restaurant,
        /// then returns the corresponding meal size data.
        /// </summary>
        /// <returns>
        /// A read-only collection of <see cref="MealSizeDTO"/> with size and pricing details.
        /// </returns>
        Task<IReadOnlyCollection<MealSizeDTO>> GetMealSizesAsync(IEnumerable<Guid> sizeIds, Guid restaurantId, CancellationToken cancellationToken);
    }
}
