using MediatR;
using Customers.Application.Interfaces;
using Customers.Application.UseCases.Queries.GetCustomerAddress;
using Customers.Contracts.DTOs;
using Customers.Contracts.Interfaces;
using Customers.Domain.Entities;
using Customers.Domain.Enums;
using Customers.Domain.ValueObjects;
using Microsoft.Extensions.Logging;


namespace Customers.Application.Services
{
    public sealed class CustomerServices(
        IMediator mediator,
        IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository,
        ILogger<CustomerServices> logger) : ICustomerServices
    {
        public async Task CreateCustomerAsync(Guid Id, CancellationToken ct)
        {
            var customer = Customer.Create(Id, Gender.Male, DateOnly.FromDateTime(DateTime.Now.AddYears(-20)), Height.FromCm(165),null);

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