using Orders.Domain.Aggregates.Carts;

namespace Orders.Application.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByCustomerIdAsync(Guid CustomerId, CancellationToken ct);
        void Add(Cart cart);
        void Remove(Cart cart);

    }
}