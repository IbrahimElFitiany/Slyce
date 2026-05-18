using Food.Application.UseCases.Queries.GetAllergens;
using Food.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Food.Infrastructure.Queries
{
    internal sealed class GetAllergensQueryHandler(FoodDbContext foodDbContext) : IRequestHandler<GetAllergensQuery, IReadOnlyCollection<AllergenResult>>
    {
        public async Task<IReadOnlyCollection<AllergenResult>> Handle(GetAllergensQuery request, CancellationToken cancellationToken)
        {
            var results = await foodDbContext.Allergens
                .Select(s => new AllergenResult(s.Id, s.Name))
                .ToListAsync(cancellationToken);

            return results.AsReadOnly();
        }
    }
}