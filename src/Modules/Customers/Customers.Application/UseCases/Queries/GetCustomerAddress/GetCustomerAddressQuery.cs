using MediatR;

namespace Customers.Application.UseCases.Queries.GetCustomerAddress
{
    public record GetCustomerAddressQuery(Guid CustomerId, Guid AddressId):IRequest<CustomerAddressResponse>;

    public record CustomerAddressResponse(
        Guid Id,
        string Label,
        string ContactNumber,
        string City,
        string Area,
        string? StreetName,
        string? StreetNumber,
        double Longitude,
        double Latitude);
}
