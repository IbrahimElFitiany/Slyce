using MediatR;

namespace Customers.Application.UseCases.Queries.ListCustomerAddresses
{
    public sealed record ListCustomerAddressesQuery(Guid CustomerId) : IRequest<ListCustomerAddressesQueryResult>;
}
