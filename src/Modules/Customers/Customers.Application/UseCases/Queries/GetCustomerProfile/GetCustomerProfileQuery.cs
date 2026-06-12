using MediatR;

namespace Customers.Application.UseCases.Queries.GetCustomerProfile
{
    public sealed record GetCustomerProfileQuery(Guid CustomerId) : IRequest<CustomerProfileResponse>;

    public sealed record CustomerProfileResponse(
        string? Gender,
        decimal? WeightKg,
        int? HeightCm,
        string ActivityRate,
        IReadOnlyCollection<Guid> Allergens,
        IReadOnlyCollection<Guid> DietPreferences);
}
