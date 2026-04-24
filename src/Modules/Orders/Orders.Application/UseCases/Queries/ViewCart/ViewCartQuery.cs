using MediatR;

namespace Orders.Application.UseCases.Queries.ViewCart
{
    public sealed record ViewCartQuery(Guid CustomerId) : IRequest<ViewCartQueryResult>;

    public sealed record ViewCartQueryResult(IReadOnlyCollection<CartItemResult> CartItems);
    public sealed record CartItemResult(
        Guid MealId,
        string MealName,
        string ImgUrl,
        string Description,
        Guid SizeId,
        string SizeName,
        int Quantity,
        decimal OriginalPrice,
        string Currency,
        decimal Calories);
}