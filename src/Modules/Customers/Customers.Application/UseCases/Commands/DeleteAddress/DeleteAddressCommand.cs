using MediatR;

namespace Customers.Application.UseCases.Commands.DeleteAddress
{
    public sealed record DeleteAddressCommand(Guid CustomerId, Guid AddressId) : IRequest;
}
