using MediatR;

namespace Orders.Application.UseCases.Queries.GetCustomerOrders
{
    public sealed record GetCustomerOrdersQuery(Guid CustomerId) : IRequest<GetCustomerOrdersResponse>;

    public sealed record GetCustomerOrdersResponse(IReadOnlyList<CustomerOrderSummary> OrdersHistory);
    public sealed record CustomerOrderSummary(
        Guid OrderId,
        string RestaurantName,
        string RestaurantLogo,
        DateTime CreatedAt,
        string OrderStatus,
        decimal TotalPrice,
        int OrderItemsCount);
}