namespace Customers.Presentation.DTOs
{
    public sealed record UpdateDietPreferencesRequest(IReadOnlyCollection<Guid> DietPreferenceIds);
}
