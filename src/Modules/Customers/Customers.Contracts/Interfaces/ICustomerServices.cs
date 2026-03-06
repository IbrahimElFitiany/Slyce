using Customers.Contracts.DTOs;

namespace Customers.Contracts.Interfaces
{
    public interface ICustomerServices
    {
        Task<CustomerAddressDTO> GetCustomerAddressByIdAsync (Guid CustomerId, Guid Addressid, CancellationToken ct = default);
    }
}
