using MediatR;

namespace Food.Application.UseCases.Queries.GetAllergens
{
    public sealed record GetAllergensQuery() : IRequest<IReadOnlyCollection<AllergenResult>>;

    public sealed record AllergenResult(Guid Id , string Name);
}