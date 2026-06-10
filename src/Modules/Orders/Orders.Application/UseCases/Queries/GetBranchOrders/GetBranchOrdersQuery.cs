using MediatR;
using Shared.Application;

namespace Orders.Application.UseCases.Queries.GetBranchOrders
{
    public sealed record GetBranchOrdersQuery(Guid BranchId, int Page, int PageSize) : IRequest<PagedResult<BranchOrderSummary>>;

    public sealed record BranchOrderSummary(
        Guid OrderId,
        DateTime CreatedAt,
        string OrderStatus,
        decimal TotalPrice,
        int OrderItemsCount);
}