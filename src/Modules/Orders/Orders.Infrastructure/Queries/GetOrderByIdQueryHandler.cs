using MediatR;
using Menus.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Orders.Application.UseCases.Queries.GetOrderById;
using Orders.Infrastructure.Persistence;
using Shared.Application.Exceptions;

namespace Orders.Infrastructure.Queries
{
    internal sealed class GetOrderByIdQueryHandler(
        OrdersDbContext ordersDbContext,
        IMenuQueryServices menuQueryServices) : IRequestHandler<GetOrderByIdQuery, GetOrderByIdResponse>
    {
        public async Task<GetOrderByIdResponse> Handle(GetOrderByIdQuery query, CancellationToken ct)
        {
            //no AuthR yet, will be fixed
            var raw = await ordersDbContext.Orders
                .Where(o => o.Id == query.Id)
                .Select(o => new
                {
                    o.Id,
                    o.CreatedAt,
                    o.PaymentMethod,
                    o.Status,
                    DeliveryAddress = new DeliveryAddress(
                        o.DeliveryAddress.City,
                        o.DeliveryAddress.Area,
                        o.DeliveryAddress.Coordinates.Longitude,
                        o.DeliveryAddress.Coordinates.Latitude),
                    Items = o.OrderItems.Select(i => new
                    {
                        i.MealId,
                        i.SizeId,
                        i.Quantity,
                        TotalPrice = i.TotalPrice.Amount
                    }).ToList()
                })
                .FirstOrDefaultAsync(ct)
                ?? throw new NotFoundException("Order", query.Id);

            var keys = raw.Items.Select(i => (i.MealId, i.SizeId));

            var snapshots = await menuQueryServices.GetMealSizeSnapshotsAsync(keys, ct);

            var orderItems = raw.Items
                .Select(i => new OrderItems(
                    snapshots.TryGetValue((i.MealId, i.SizeId), out var snap) ? snap.MealName : "Unknown",
                    i.Quantity,
                    i.TotalPrice))
                .ToList();

            return new GetOrderByIdResponse(
                raw.Id,
                DateOnly.FromDateTime(raw.CreatedAt),
                raw.DeliveryAddress,
                orderItems,
                raw.Status.ToString(),
                raw.PaymentMethod.ToString());
        }
    }
}