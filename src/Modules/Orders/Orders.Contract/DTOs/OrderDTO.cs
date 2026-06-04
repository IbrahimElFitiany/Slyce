using Orders.Contract.Interfaces;

namespace Orders.Contract.DTOs
{
    public sealed record OrderDTO(
        Guid CustomerId,
        Guid BranchId,
        IEnumerable<OrderItemDTO> OrderItemDTOs);

    public sealed record OrderItemDTO(Guid MealId, Guid SizeId, int Qty);
}
