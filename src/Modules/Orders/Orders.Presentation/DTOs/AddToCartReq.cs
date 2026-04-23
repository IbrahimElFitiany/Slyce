namespace Orders.Presentation.DTOs
{
    public sealed record AddToCartReq(
        Guid RestaurantId,
        Guid MealId,
        Guid SizeId,
        int Quantity);
}
