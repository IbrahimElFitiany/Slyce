using MediatR;
using Customers.Application.Interfaces;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;
using Customers.Domain.Entities;
using Microsoft.Extensions.Logging;


namespace Customers.Application.UseCases.Commands.CreateAddress
{
    public sealed class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand,Guid>
    {
        private readonly ILogger<CreateAddressCommandHandler> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAddressCommandHandler(
            ILogger<CreateAddressCommandHandler> logger,
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);

            if (customer is null) 
                throw new NotFoundException(nameof(Customer), request.CustomerId);

            var address = MapToCustomerAddress(request);

            customer.AddAddress(address);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Customer {CustomerId} added a new address {AddressId} with label {Label}",
                customer.Id, address.Id, address.Label);

            return address.Id;
        }

        private CustomerAddress MapToCustomerAddress(CreateAddressCommand command)
        {
            return new CustomerAddress(
                command.Label,
                PhoneNumber.Create(command.ContactNumber),
                new Address(
                    command.City,
                    command.Area,
                    command.StreetName,
                    command.StreetNumber,
                    new Coordinates(
                        command.Latitude,
                        command.Longitude)));
        }
    }
}