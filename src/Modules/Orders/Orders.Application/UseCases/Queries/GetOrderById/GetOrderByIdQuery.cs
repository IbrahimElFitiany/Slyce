using MediatR;

namespace Orders.Application.UseCases.Queries.GetOrderById
{
    public sealed record GetOrderByIdQuery(Guid Id) : IRequest<GetOrderByIdResponse>;

    public sealed record GetOrderByIdResponse(
        Guid OrderId,
        DateOnly CreatedAt,
        DeliveryAddress DeliveryAddress,
        IReadOnlyList<OrderItems> OrderItems,
        string OrderStatus,
        string PaymentMethod);

    public sealed record DeliveryAddress(
        string City,
        string Area,
        double Lng,
        double Lat);

    public sealed record OrderItems(
        string MealName,
        int Quantity,
        decimal PriceAtOrder);
}
