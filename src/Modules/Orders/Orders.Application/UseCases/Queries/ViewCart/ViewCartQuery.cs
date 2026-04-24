using MediatR;

namespace Orders.Application.UseCases.Queries.ViewCart
{
    public sealed record ViewCartQuery(Guid CustomerId) : IRequest<ViewCartQueryResponse>;

    public sealed record ViewCartQueryResponse(IReadOnlyCollection<CartItemResponse> CartItems);
    public sealed record CartItemResponse(
        string Id,
        string ImgUrl,
        string Name,
        string Description,
        int Quantity,
        decimal OriginalPrice,
        decimal? DiscountedPrice);
}