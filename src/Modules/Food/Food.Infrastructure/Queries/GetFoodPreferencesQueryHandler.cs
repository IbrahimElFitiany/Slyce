using Food.Application.UseCases.Queries.GetFoodPreferences;
using Food.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Food.Infrastructure.Queries
{
    internal sealed class GetFoodPreferencesQueryHandler(FoodDbContext foodDbContext) : IRequestHandler<GetFoodPreferencesQuery, IReadOnlyCollection<FoodPreferenceResult>>
    {
        public async Task<IReadOnlyCollection<FoodPreferenceResult>> Handle(GetFoodPreferencesQuery request, CancellationToken cancellationToken)
        {
            var results = await foodDbContext.FoodPreferences
                .Select(s => new FoodPreferenceResult(s.Id, s.Name))
                .ToListAsync(cancellationToken);

            return results.AsReadOnly();
        }
    }
}