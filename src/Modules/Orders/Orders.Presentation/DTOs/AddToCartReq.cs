namespace Orders.Presentation.DTOs
{
    public sealed record AddToCartReq(
        Guid BranchId,
        Guid MealId,
        Guid SizeId,
        int Quantity);
}
