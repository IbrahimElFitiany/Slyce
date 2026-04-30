using Food.Application.UseCases.Queries;
using Food.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application;

namespace Food.Infrastructure.Queries
{
    public sealed class SearchFoodSummaryQueryHandler (FoodDbContext foodDbContext) :
        IRequestHandler<SearchFoodSummaryQuery, PagedResult<SearchFoodSummaryQueryResult>>
    {
        private readonly FoodDbContext _foodDbContext = foodDbContext;
        
        public async Task<PagedResult<SearchFoodSummaryQueryResult>> Handle(
            SearchFoodSummaryQuery searchQuery,
            CancellationToken ct)
        {
            //rn just using Postgris for development speed 
            //considering ElasticSearch with cdc to move the food data to es 

            var term = $"%{searchQuery.Term}%";

            var query = _foodDbContext.Foods
                .Where(f => EF.Functions.ILike(f.Name, term));

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(f => f.Id)
                .Skip((searchQuery.PageNumber - 1) * searchQuery.PageSize)
                .Take(searchQuery.PageSize)
                .Select(f => new SearchFoodSummaryQueryResult(
                    f.Id,
                    f.Image,
                    f.Name,
                    f.NutritionPer100g.Calories,
                    f.NutritionPer100g.TotalFat,
                    f.NutritionPer100g.Protein,
                    f.NutritionPer100g.TotalCarbohydrate))
                .ToListAsync(ct);

            var totalPages = (int)Math.Ceiling((double)total / searchQuery.PageSize);

            return new PagedResult<SearchFoodSummaryQueryResult>(
                Total: total,
                PerPage: searchQuery.PageSize,
                CurrentPage: searchQuery.PageNumber,
                TotalPages: totalPages,
                HasPrevious: searchQuery.PageNumber > 1,
                HasNext: searchQuery.PageNumber < totalPages,
                Items: items) ;
        }
    }
}