using Customers.Application.UseCases.Queries.GetCustomerAddress;
using Customers.Contracts.DTOs;
using Customers.Contracts.Interfaces;
using MediatR;

namespace Customers.Application.Services
{
    public sealed class CustomerServices : ICustomerServices
    {
        private readonly IMediator _mediator;

        public CustomerServices(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CustomerAddressDTO> GetCustomerAddressByIdAsync(Guid CustomerId, Guid Addressid, CancellationToken ct = default)
        {
            var query = new GetCustomerAddressQuery(CustomerId, Addressid);

            var customerAddress = await _mediator.Send(query, ct);

            return new CustomerAddressDTO(
                Id: customerAddress.Id,
                ContactNumber: customerAddress.ContactNumber,
                City: customerAddress.City,
                Area: customerAddress.Area,
                StreetName: customerAddress.StreetName,
                StreetNumber: customerAddress.StreetNumber,
                customerAddress.Longitude,
                customerAddress.Latitude);
        }
    }
}
