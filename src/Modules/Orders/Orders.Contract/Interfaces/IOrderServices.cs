using Orders.Contract.DTOs;

namespace Orders.Contract.Interfaces
{
    public interface IOrderServices
    {
        Task CreateOrdersAsync(IEnumerable<OrderDTO> orderDTOs, CancellationToken ct);
    }
}