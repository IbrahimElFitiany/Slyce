using MediatR;

namespace Orders.Application.UseCases.Queries.GetOrdersTrendByBranch
{
    public enum PeriodType
    {
        Today,
        Yesterday,
        Past7Days,
        Past30Days
    }

    public sealed record GetOrdersTrendByBranchQuery(Guid BranchId, PeriodType Period) : IRequest<OrdersTrendResponse>;

    
    public sealed record OrdersTrendResponse(SummaryTotals Summary, IReadOnlyCollection<TrendDataPoint> ChartData);

    public sealed record SummaryTotals(int TotalOrders, decimal TotalRevenue);

    public sealed record TrendDataPoint(string Timestamp, int Orders);
}