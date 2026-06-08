using MediatR;
using Microsoft.EntityFrameworkCore;
using Orders.Application.UseCases.Queries.GetOrdersTrendByBranch;
using Orders.Infrastructure.Persistence;
using System.Globalization;

namespace Orders.Infrastructure.Queries
{
    internal sealed class GetOrdersTrendByBranchQueryHandler(OrdersDbContext ordersDbContext)
        : IRequestHandler<GetOrdersTrendByBranchQuery, OrdersTrendResponse>
    {
        public async Task<OrdersTrendResponse> Handle(GetOrdersTrendByBranchQuery query, CancellationToken ct)
        {
            var todayUtc = DateTime.UtcNow.Date;

            var (startDate, endDate, isHourlyGrouping) = query.Period switch
            {
                PeriodType.Today => (todayUtc, todayUtc.AddDays(1), true),
                PeriodType.Yesterday => (todayUtc.AddDays(-1), todayUtc, true),
                PeriodType.Past7Days => (todayUtc.AddDays(-6), DateTime.UtcNow, false),
                PeriodType.Past30Days => (todayUtc.AddDays(-29), DateTime.UtcNow, false),
                _ => throw new ArgumentOutOfRangeException(nameof(query.Period))
            };

            var dbData = await FetchRawMetricsAsync(query.BranchId, startDate, endDate, isHourlyGrouping, ct);

            var totalOrders = dbData.Sum(d => d.OrderCount);
            var totalRevenue = dbData.Sum(d => d.Revenue);

            var chartPoints = new List<TrendDataPoint>();

            if (isHourlyGrouping)
            {
                for (int hour = 0; hour < 24; hour++)
                {
                    var match = dbData.FirstOrDefault(d => d.Hour == hour);
                    chartPoints.Add(new TrendDataPoint($"{hour:D2}:00", match?.OrderCount ?? 0));
                }
            }
            else if (query.Period == PeriodType.Past7Days)
            {
                for (int i = 0; i < 7; i++)
                {
                    var targetDate = startDate.AddDays(i);
                    var match = dbData.FirstOrDefault(d => d.Date == targetDate);
                    chartPoints.Add(new TrendDataPoint(targetDate.ToString("ddd", CultureInfo.InvariantCulture), match?.OrderCount ?? 0));
                }
            }
            else 
            {
                for (int i = 0; i < 30; i++)
                {
                    var targetDate = startDate.AddDays(i);
                    var match = dbData.FirstOrDefault(d => d.Date == targetDate);
                    chartPoints.Add(new TrendDataPoint(targetDate.Day.ToString(), match?.OrderCount ?? 0));
                }
            }

            return new OrdersTrendResponse(
                Summary: new SummaryTotals(totalOrders, totalRevenue),
                ChartData: chartPoints
            );
        }

        private async Task<List<DbMetricRow>> FetchRawMetricsAsync(Guid branchId, DateTime start, DateTime end, bool isHourly, CancellationToken ct)
        {
            var baseQuery = ordersDbContext.Orders
                .Where(o => o.BranchId == branchId && o.CreatedAt >= start && o.CreatedAt < end);

            if (isHourly)
            {
                return await baseQuery
                    .GroupBy(o => o.CreatedAt.Hour)
                    .Select(g => new DbMetricRow(g.Key, null, g.Count(), g.Sum(o => o.TotalPrice.Amount)))
                    .ToListAsync(ct);
            }

            return await baseQuery
                .GroupBy(o => o.CreatedAt.Date)
                .Select(g => new DbMetricRow(null, g.Key, g.Count(), g.Sum(o => o.TotalPrice.Amount)))
                .ToListAsync(ct);
        }

        private sealed record DbMetricRow(int? Hour, DateTime? Date, int OrderCount, decimal Revenue);
    }
}