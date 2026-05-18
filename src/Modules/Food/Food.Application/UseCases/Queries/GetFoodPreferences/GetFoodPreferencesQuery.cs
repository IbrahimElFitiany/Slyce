using MediatR;

namespace Food.Application.UseCases.Queries.GetFoodPreferences
{
    public sealed record GetFoodPreferencesQuery() : IRequest<IReadOnlyCollection<FoodPreferenceResult>>;

    public sealed record FoodPreferenceResult(Guid Id, string Name);
}