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
        Task<IReadOnlyCollection<MealSizeDTO>> GetMealSizesByRestaurantAsync(IEnumerable<Guid> sizeIds, Guid restaurantId, CancellationToken cancellationToken);

        /// <summary>
        /// Returns a summary of the specified meal, or <see langword="null"/> if no meal with the given ID exists.
        /// </summary>
        Task<MealSummaryDTO?> GetMealSummaryAsync(Guid mealId, CancellationToken cancellationToken);

        //TODO: Organize the Interface and generalize it 
        Task<IReadOnlyDictionary<(Guid MealId, Guid SizeId), MealSizeInfoDTO>> GetMealSizeSnapshotsAsync(
            IEnumerable<(Guid MealId, Guid SizeId)> keys,
            CancellationToken cancellationToken);

    }
}
