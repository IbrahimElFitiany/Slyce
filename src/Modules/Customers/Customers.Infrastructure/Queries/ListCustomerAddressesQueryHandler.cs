using MediatR;
using Microsoft.EntityFrameworkCore;
using Customers.Infrastructure.Persistence;
using Customers.Application.UseCases.Queries.ListCustomerAddresses;

namespace Customers.Infrastructure.Queries
{
    internal sealed class ListCustomerAddressesQueryHandler(CustomersDbContext customersDbContext) : IRequestHandler<ListCustomerAddressesQuery, ListCustomerAddressesQueryResult>
    {
        private readonly CustomersDbContext _customersDbContext = customersDbContext;

        public async Task<ListCustomerAddressesQueryResult> Handle(ListCustomerAddressesQuery query, CancellationToken ct)
        {

            var addresses = await _customersDbContext.Customers
                .AsNoTracking()
                .Where(c => c.Id == query.CustomerId)
                .SelectMany(c => c.CustomerAddresses)
                .Select(a => new CustomerAddressResult(
                    a.Id,
                    a.Label,
                    a.IsPrimary,
                    a.ContactNumber.Value,
                    a.Address.City,
                    a.Address.Area,
                    a.Address.StreetName,
                    a.Address.StreetNumber,
                    a.Address.Coordinates.Latitude,
                    a.Address.Coordinates.Longitude
                ))
                .ToListAsync(ct);


            return new ListCustomerAddressesQueryResult(addresses);
        }
    }
}