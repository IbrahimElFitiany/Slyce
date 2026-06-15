using MediatR;
using Customers.Application.Interfaces;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;
using Customers.Domain.Entities;
using Microsoft.Extensions.Logging;
using Customers.Domain.Repositories;


namespace Customers.Application.UseCases.Commands.CreateAddress
{
    internal sealed class CreateAddressCommandHandler(
        ILogger<CreateAddressCommandHandler> logger,
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateAddressCommand,Guid>
    {

        public async Task<Guid> Handle(CreateAddressCommand command, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetByIdAsync(command.CustomerId, cancellationToken)
                ?? throw new NotFoundException("Customer", command.CustomerId);

            var address =  new CustomerAddress(
                label: command.Label,
                contactNumber: PhoneNumber.Create(command.ContactNumber),
                address: new Address(
                    command.City,
                    command.Area,
                    command.StreetName,
                    command.StreetNumber,
                    new Coordinates(
                        command.Latitude,
                        command.Longitude)));

            customer.AddAddress(address);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Customer {CustomerId} added a new address {AddressId} with label {Label}",
                customer.Id, address.Id, address.Label);

            return address.Id;
        }
    }
}