namespace Customers.Presentation.DTOs
{
    public sealed record UpdateAllergensRequest(IReadOnlyCollection<Guid> AllergenIds);
}