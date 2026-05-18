using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Application.UseCases.Queries.GetBranchDetails;
using Restaurants.Infrastructure.Persistence;
using Shared.Application.Exceptions;

namespace Restaurants.Infrastructure.Queries
{
    internal sealed class GetBranchDetailsQueryHandler(
        RestaurantDbContext dbContext,
        ILogger<GetBranchDetailsQueryHandler> logger) : IRequestHandler<GetBranchDetailsQuery, GetBranchDetailsResult>
    {
        public async Task<GetBranchDetailsResult> Handle(GetBranchDetailsQuery query, CancellationToken ct)
        {
            var branch = await dbContext.RestaurantBranches
                .Where(b => b.Id == query.BranchId)
                .FirstOrDefaultAsync(ct)
                ?? throw new NotFoundException("branch", query.BranchId);

            return new GetBranchDetailsResult(
                branch.Address.City,
                branch.Address.Area,
                branch.PhoneNumber.Value,
                branch.Schedule
                    .OrderBy(wh => wh.Day)
                    .Select(wh => new BranchWorkingHoursResult(wh.Day, wh.OperatingHours.OpeningTime, wh.OperatingHours.ClosingTime))
                    .ToList()
            );
        }
    }
}