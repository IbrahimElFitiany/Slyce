using Food.Application.DTOs;
using MediatR;

namespace Food.Application.UseCases.Commands.ImportExternalFood
{
    public sealed record ImportExternalFoodsCommand(string SearchTerm):IRequest<IEnumerable<FoodSummaryDTO>>;
}
