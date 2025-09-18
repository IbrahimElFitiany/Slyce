using Orders.Domain.Entities;

namespace Orders.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(Guid id);
        Task UpdateStatusAsync(Order order);
    }
}
