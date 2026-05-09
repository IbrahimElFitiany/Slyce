using MediatR;

namespace Restaurants.Application.UseCases.Commands.ActivateBranch
{
    public sealed record ActivateBranchCommand(Guid BranchId) : IRequest;
}
