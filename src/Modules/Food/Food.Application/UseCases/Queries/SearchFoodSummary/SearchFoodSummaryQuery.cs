using MediatR;
using Shared.Application;

namespace Food.Application.UseCases.Queries.SearchFoodSummary
{
    public sealed record SearchFoodSummaryQuery(
        string Term,
        int PageNumber,
        int PageSize) : IRequest<PagedResult<SearchFoodSummaryQueryResult>>;
}
