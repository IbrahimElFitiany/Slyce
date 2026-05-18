using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurants.Contracts.Interfaces;
using Shared.Application;
using Subscriptions.Application.UseCases.Queries.GetCustomerSubscriptionsSummary;
using Subscriptions.Domain.Enums;
using Subscriptions.Infrastructure.Persistence;

namespace Subscriptions.Infrastructure.Queries
{
    internal sealed class GetCustomerSubscriptionsQueryHandler(
        SubscriptionsDbContext dbContext,
        IRestaurantQueryServices restaurantQueryServices) : IRequestHandler<GetCustomerSubscriptionsSummaryQuery, PagedResult<CustomerSubscriptionSummary>>
    {
        public async Task<PagedResult<CustomerSubscriptionSummary>> Handle(GetCustomerSubscriptionsSummaryQuery request, CancellationToken ct)
        {
            var baseQuery = dbContext.Subscriptions
                .AsNoTracking()
                .Where(s => s.CustomerId == request.CustomerId);

            baseQuery = request.SortDescending ? baseQuery.OrderByDescending(s => s.CreatedAt) : baseQuery.OrderBy(s => s.CreatedAt);

            if (request.Status is not null)
                baseQuery = baseQuery.Where(s => s.Status == Enum.Parse<SubscriptionStatus>(request.Status, true));

            var totalSubs = await baseQuery.CountAsync(ct);

            if (totalSubs == 0)
                return PagedResult<CustomerSubscriptionSummary>.Empty(request.Page, request.PageSize);

            var subscriptions = await baseQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(s => new
                {
                    s.Id,
                    s.BranchId,
                    s.CreatedAt,
                    s.TotalPrice,
                    s.Status,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    MealCount = s.SubscriptionMeals.Count,
                    DeliveryDays = s.DeliveryDays.Select(d => d.Day).ToList()
                })
                .ToListAsync(ct);

            var branchIds = subscriptions.Select(s => s.BranchId).Distinct();

            var branches = await restaurantQueryServices.GetBranchInfosAsync(branchIds, ct);

            var summaries = subscriptions.Select(s =>
            {
                var branch = branches.GetValueOrDefault(s.BranchId);
                return new CustomerSubscriptionSummary(
                    SubscriptionId: s.Id,
                    RestaurantId: branch.RestaurantId,
                    RestaurantName: branch.RestaurantName,
                    RestaurantLogoUrl: branch.LogoUrl,
                    NextDelivery: GetNextDelivery(s.DeliveryDays, s.Status, s.StartDate, s.EndDate),
                    StartDate: s.StartDate,
                    TotalItems: s.MealCount,
                    TotalPriceAmount: s.TotalPrice.Amount,
                    PriceCurrency: s.TotalPrice.Currency,
                    Status: s.Status.ToString());
            }).ToList();

            var totalPages = (int)Math.Ceiling(totalSubs / (double)request.PageSize);

            return new PagedResult<CustomerSubscriptionSummary>(
                Total: totalSubs,
                PerPage: request.PageSize,
                CurrentPage: request.Page,
                TotalPages: totalPages,
                HasNext: request.Page < totalPages,
                HasPrevious: request.Page > 1,
                Items: summaries);
        }

        private static DateOnly? GetNextDelivery(IEnumerable<DayOfWeek> days, SubscriptionStatus status, DateOnly startDate, DateOnly endDate)
        {
            if (status != SubscriptionStatus.Active)
                return null;

            var set = days.ToHashSet();
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            for (int i = 1; i <= 7; i++)
            {
                var candidate = today.AddDays(i);
                if (candidate < startDate || candidate > endDate)
                    continue;
                if (set.Contains(candidate.DayOfWeek))
                    return candidate;
            }

            return null;
        }
    }
}