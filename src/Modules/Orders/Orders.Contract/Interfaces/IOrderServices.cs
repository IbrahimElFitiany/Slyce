using Orders.Contract.DTOs;

namespace Orders.Contract.Interfaces
{
    public interface IOrderServices
    {
        Task CreateOrderAsync(OrderDTO orderDTO, CancellationToken cancellationToken = default);
    }
}