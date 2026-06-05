namespace Orders.Contract.DTOs
{
    public sealed record OrderDTO (
        Guid CustomerId,
        Guid BranchId,
        Guid SubscriptionId,
        IEnumerable<OrderItemDTO> OrderItems,
        string City,
        string Area,
        string? StreetName,
        string? StreetNumber,
        double Latitude,
        double Longitude,
        TimeOnly From,
        TimeOnly To);

    public sealed record OrderItemDTO(Guid MealId, Guid SizeId, int Qty, decimal Price);
}
