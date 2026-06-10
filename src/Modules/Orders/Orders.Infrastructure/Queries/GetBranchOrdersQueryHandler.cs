using Microsoft.EntityFrameworkCore;
using MediatR;
using Orders.Application.UseCases.Queries.GetBranchOrders;
using Orders.Infrastructure.Persistence;
using Shared.Application;

namespace Orders.Infrastructure.Queries
{
    internal sealed class GetBranchOrdersQueryHandler(
        OrdersDbContext ordersDbContext) : IRequestHandler<GetBranchOrdersQuery, PagedResult<BranchOrderSummary>>
    {
        public async Task<PagedResult<BranchOrderSummary>> Handle(GetBranchOrdersQuery request, CancellationToken ct)
        {
            var baseQuery = ordersDbContext.Orders
                .AsNoTracking()
                .Where(o => o.BranchId == request.BranchId);

            var totalCount = await baseQuery.CountAsync(ct);

            if (totalCount == 0)
            {
                return PagedResult<BranchOrderSummary>.Empty(request.Page, request.PageSize);
            }

            var orders = await baseQuery
                .OrderByDescending(o => o.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(o => new BranchOrderSummary(
                    o.Id,
                    o.CreatedAt,
                    o.Status.ToString(),
                    o.TotalPrice.Amount,
                    o.OrderItems.Count))
                .ToListAsync(ct);

            int totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);
            bool hasNext = request.Page < totalPages;
            bool hasPrevious = request.Page > 1;

            return new PagedResult<BranchOrderSummary>(
                totalCount,
                request.PageSize,
                request.Page,
                totalPages,
                hasNext,
                hasPrevious,
                orders);
        }
    }
}