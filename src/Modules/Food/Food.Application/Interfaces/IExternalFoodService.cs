using Food.Application.DTOs;

namespace Food.Application.Interfaces
{
    public interface IExternalFoodService
    {
        /// <summary>
        /// Searches an external food provider for foods matching the given term,
        /// returning nutrition facts normalized to per-100g values.
        /// </summary>
        /// <returns>A collection of matched foods with their nutritional breakdown per 100g.</returns>
        Task<IEnumerable<ExternalFoodResultDTO>> SearchAsync(string searchTerm, CancellationToken cancellationToken);
    }
}
