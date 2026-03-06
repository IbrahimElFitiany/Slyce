using MediatR;
using Microsoft.EntityFrameworkCore;
using Customers.Infrastructure.Persistence;
using Customers.Application.UseCases.Queries.GetCustomerAddress;
using Shared.Application.Exceptions;
using Customers.Domain.Entities;

namespace Customers.Infrastructure.Queries
{
    public sealed class GetCustomerAddressQueryHandler : IRequestHandler<GetCustomerAddressQuery, CustomerAddressResponse>
    {
        private readonly CustomersDbContext _customersDbContext;

        public GetCustomerAddressQueryHandler(CustomersDbContext customersRead)
        {
            _customersDbContext = customersRead;
        }

        public async Task<CustomerAddressResponse> Handle(GetCustomerAddressQuery query, CancellationToken cancellationToken)
        {
            // CustomerAddress is an owned type; queried via Customer aggregate (DB join).
            // See docs: /Modules/Customers/Usecases/GetCustomerAddress

            var address = await _customersDbContext.Customers
            .AsNoTracking()
            .Where(c => c.Id == query.CustomerId)
            .SelectMany(c => c.CustomerAddresses)
            .Where(a => a.Id == query.AddressId)
            .Select(a => new CustomerAddressResponse(
                a.Id,
                a.Label,
                a.ContactNumber.Value,
                a.Address.City,
                a.Address.Area,
                a.Address.StreetName,
                a.Address.StreetNumber,
                a.Address.Coordinates.Longitude,
                a.Address.Coordinates.Latitude
            ))
            .FirstOrDefaultAsync(cancellationToken);

            if (address is null)
                throw new NotFoundException(nameof(CustomerAddress), query.AddressId);

            return address;
        }
    }
}