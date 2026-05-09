using MediatR;
using Customers.Application.Interfaces;
using Customers.Application.UseCases.Queries.GetCustomerAddress;
using Customers.Contracts.DTOs;
using Customers.Contracts.Interfaces;
using Customers.Domain.Entities;
using Microsoft.Extensions.Logging;


namespace Customers.Application.Services
{
    public sealed class CustomerServices(
        IMediator mediator,
        IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository,
        ILogger<CustomerServices> logger) : ICustomerServices
    {
        public async Task CreateCustomerAsync(Guid Id, DateOnly Birthday, CancellationToken ct)
        {
            var customer = Customer.Create(Id, null, Birthday, null, null);

            customerRepository.Add(customer);
            await unitOfWork.SaveChangesAsync(ct);

            logger.LogInformation("Customer Profile for User: {UserId} Created", customer.Id);
        }

        public async Task<CustomerAddressDTO> GetCustomerAddressByIdAsync(Guid CustomerId, Guid Addressid, CancellationToken ct = default)
        {
            var query = new GetCustomerAddressQuery(CustomerId, Addressid);

            var customerAddress = await mediator.Send(query, ct);

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