using MediatR;
using Microsoft.EntityFrameworkCore;
using Orders.Application.UseCases.Queries.GetCustomerOrders;
using Orders.Infrastructure.Persistence;
using Restaurants.Contracts.Interfaces;

namespace Orders.Infrastructure.Queries
{
    internal sealed class GetCustomerOrdersQueryHandler(OrdersDbContext ordersDbContext, IRestaurantQueryServices restaurantQueryServices) : IRequestHandler<GetCustomerOrdersQuery, GetCustomerOrdersResponse>
    {
        public async Task<GetCustomerOrdersResponse> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await ordersDbContext.Orders
                .Where(o => o.CustomerId == request.CustomerId)
                .Select(o => new
                {
                    o.Id,
                    o.BranchId,
                    o.CreatedAt,
                    o.Status,
                    o.TotalPrice.Amount,
                    ItemCount = o.OrderItems.Count
                }).ToListAsync(cancellationToken);

            var branchIds = orders.Select(o => o.BranchId).Distinct().ToList();
            var restaurants = await restaurantQueryServices.GetBranchInfosAsync(branchIds, cancellationToken);

            var result = orders.Select(o =>
            {
                var restaurant = restaurants[o.BranchId];
                return new CustomerOrderSummary(
                    o.Id,
                    restaurant.RestaurantName,
                    restaurant.LogoUrl,
                    o.CreatedAt,
                    o.Status.ToString(),
                    o.Amount,
                    o.ItemCount);
            }).ToList();

            return new GetCustomerOrdersResponse(result);
        }
    }
}