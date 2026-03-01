using MediatR;

namespace Customers.Application.UseCases.Commands.CreateAddress
{
    public sealed record CreateAddressCommand(
        Guid CustomerId,
        string Label,
        string StreetName,
        string StreetNumber,
        string Area,
        string City,
        double Latitude,
        double Longitude,
        string ContactNumber):IRequest<Guid>;
}
